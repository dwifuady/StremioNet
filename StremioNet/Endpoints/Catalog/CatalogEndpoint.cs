using StremioNet.Common;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Catalog;

public interface ICatalogEndpoint
{
    Task<IEnumerable<Catalog>> GetCatalogs();
    Task<IEnumerable<DiscoverDto>> GetDiscoverAsync(GetDiscoverRequest request);
    Task<IEnumerable<DiscoverDto>> GetSearchAsync(SearchRequest request);
    IAsyncEnumerable<DiscoverDto> StreamDiscoverAsync(GetDiscoverRequest request);
    IAsyncEnumerable<DiscoverDto> StreamSearchAsync(SearchRequest request);
}

public class CatalogEndpoint : EndpointBase, ICatalogEndpoint
{
    internal CatalogEndpoint(Stremio stremio) : base(stremio)
    {
    }

    public async Task<IEnumerable<Catalog>> GetCatalogs()
    {
        var catalogs = new List<Catalog>();

        foreach (var catalogAddon in CatalogAddons.Select(c => c.Catalogs))
        {
            if (catalogAddon != null)
            {
                catalogs.AddRange(catalogAddon);
            }
        }

        return catalogs;
    }

    public async Task<IEnumerable<DiscoverDto>> GetDiscoverAsync(GetDiscoverRequest request)
    {
        var catalogs = await GetCatalogs();
        var results = new List<DiscoverDto>();
        foreach (var addonCatalogRequest in request.AddonCatalogRequests)
        {
            foreach (var catalogRequest in addonCatalogRequest.CatalogGenres)
            {
                results.AddRange(await GetDiscoverResultsAsync(addonCatalogRequest.AddonId, catalogRequest, catalogs));
            }
        }

        return results;
    }

    public async Task<IEnumerable<DiscoverDto>> GetSearchAsync(SearchRequest request)
    {
        var catalogs = await GetCatalogs();
        var results = new List<DiscoverDto>();
        if (string.IsNullOrEmpty(request.Query))
        {
            throw new ArgumentNullException(nameof(request.Query), $"{nameof(request.Query)} is missing from the parameters");
        }

        var addons =
            CatalogAddons.Where(c =>
                c.IsTypeSupported(request.TypeEnum) &&
                c.Catalogs != null &&
                c.Catalogs.Any(ca => ca.Searchable));

        foreach (var addon in addons)
        {
            foreach (var addonCatalog in addon.Catalogs!)
            {
                var catalog = catalogs.FirstOrDefault(c => c.Id == addonCatalog.Id);
                DiscoverDto result;
                try
                {
                    result = await addon.Api.SearchFromCatalog(request.Type, addonCatalog.Id, request.Query);
                    result.AddonId = addon.Id;
                    result.AddonName = addon.Name;
                    result.CatalogName = catalog?.Name;
                    result.Type = request.Type;
                }
                catch (Exception e)
                {
                    // todo : return Result with status, so we can send the error instead of skipping it
                    continue;
                }

                results.Add(result);
            }
        }

        return results;
    }

    public async IAsyncEnumerable<DiscoverDto> StreamDiscoverAsync(GetDiscoverRequest request)
    {
        var catalogs = await GetCatalogs();

        foreach (var addonCatalogRequest in request.AddonCatalogRequests)
        {
            foreach (var catalogRequest in addonCatalogRequest.CatalogGenres)
            {
                await foreach (var result in GetDiscoverResultsStreamAsync(addonCatalogRequest.AddonId, catalogRequest, catalogs))
                {
                    if (result != null)
                    {
                        yield return result;
                    }
                }
            }
        }
    }

    public async IAsyncEnumerable<DiscoverDto> StreamSearchAsync(SearchRequest request)
    {
        
        if (string.IsNullOrEmpty(request.Query))
        {
            throw new ArgumentNullException(nameof(request.Query), $"{nameof(request.Query)} is missing from the parameters");
        }

        var addons =
            CatalogAddons.Where(c => 
                c.IsTypeSupported(request.TypeEnum) && 
                c.Catalogs != null && 
                c.Catalogs.Any(ca => ca.Searchable));
        
        var catalogs = await GetCatalogs();

        foreach (var addon in addons)
        {
            foreach (var addonCatalog in addon.Catalogs!.Where(c => c.Searchable))
            {
                var catalog = catalogs.FirstOrDefault(c => c.Id == addonCatalog.Id);

                DiscoverDto result;
                try
                {
                    result = await addon.Api.SearchFromCatalog(request.Type, addonCatalog.Id, request.Query);
                    result.AddonId = addon.Id;
                    result.AddonName = addon.Name;
                    result.CatalogName = catalog?.Name;
                    result.Type = request.Type;
                }
                catch (Exception e)
                {
                    // todo : return Result with status, so we can send the error instead of skipping it
                    continue;
                }

                yield return result;
            }
        }
    }

    private async IAsyncEnumerable<DiscoverDto?> GetDiscoverResultsStreamAsync(string addonId, CatalogRequest catalogRequest, IEnumerable<Catalog> catalogs)
    {
        var catalog = catalogs.FirstOrDefault(c => c.Id == catalogRequest.CatalogId);
        var addon = GetAddonForRequest(addonId, catalogRequest.TypeEnum, catalogRequest.Genres);
        if (addon == null)
            yield break;

        var result = new DiscoverDto();
        try
        {
            if (string.IsNullOrEmpty(catalogRequest.Genres))
            {
                result = await addon.Api.GetCatalogByCatalogId(catalogRequest.Type, catalogRequest.CatalogId);
            }
            else
            {
                result = await addon.Api.GetCatalogByGenre(catalogRequest.Type, catalogRequest.CatalogId, catalogRequest.Genres);
            }

            result.AddonId = addon.Id;
            result.AddonName = addon.Name;
            result.CatalogName = catalog?.Name;
            result.Type = catalogRequest.Type;
            result.Genre = catalogRequest.Genres;
        }
        catch (Exception e)
        {
            // todo : return Result with status, so we can send the error instead of skipping it
            
        }
        yield return result;
    }

    private async Task<IEnumerable<DiscoverDto>> GetDiscoverResultsAsync(string addonId, CatalogRequest catalogRequest, IEnumerable<Catalog> catalogs)
    {
        var results = new List<DiscoverDto>();
        await foreach (var result in GetDiscoverResultsStreamAsync(addonId, catalogRequest, catalogs))
        {
            if (result != null)
            {
                results.Add(result);
            }
        }
        return results;
    }

    private CatalogAddons? GetAddonForRequest(string addonId, CatalogTypeEnum typeEnum, string? genres)
    {
        return CatalogAddons.FirstOrDefault(c =>
            c.Id == addonId &&
            c.IsTypeSupported(typeEnum) &&
            c.Catalogs != null &&
            (!string.IsNullOrEmpty(genres) || c.Catalogs.Any(ca => !ca.GenreRequired)));
    }
}