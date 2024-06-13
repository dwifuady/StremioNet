using StremioNet.Common;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Meta;

public interface IMetaEndpoint : IEndpointBase<DetailDto>
{

}

public class MetaEndpoint : EndpointBase, IMetaEndpoint
{
    internal MetaEndpoint(Stremio stremio) : base(stremio)
    {
    }

    public async Task<IEnumerable<DetailDto>> GetResultsAsync(CatalogTypeEnum type, string id)
    {
        List<DetailDto> results = [];

        foreach (var addon in MetaAddons.Where(m => m.IsTypeSupported(type)))
        {
            var result = await addon.Api.GetMeta(AddonConstants.CatalogTypeDict[type], id);
            result.AddonId = addon.Id;
            result.AddonName = addon.Name;

            results.Add(result);
        }

        return results;
    }

    public async IAsyncEnumerable<DetailDto> StreamResultsAsync(CatalogTypeEnum type, string id)
    {
        foreach (var addon in MetaAddons.Where(m => m.IsTypeSupported(type)))
        {
            var result = await addon.Api.GetMeta(AddonConstants.CatalogTypeDict[type], id);

            result.AddonId = addon.Id;
            result.AddonName = addon.Name;

            yield return result;
        }
    }

}