using System.Text.Json;
using Refit;
using StremioNet.Common;
using StremioNet.Dtos.Manifest;

namespace StremioNet.Endpoints.Catalog;

public class CatalogAddons(
    AddonDto addon) : StremioAddons(addon)
{
    public ICatalogApi Api { get; } = RestService.For<ICatalogApi>(addon.BaseUri);

    public List<string>? IdPrefixes { get; } = addon.Manifest.IdPrefixes;

    public List<Catalog>? Catalogs
    {
        get
        {
            if (addon.Manifest.Catalogs == null) 
                return null;

            var catalogs = new List<Catalog>();
            
            foreach (var manifestCatalog in addon.Manifest.Catalogs)
            {
                var extraGenres =
                    manifestCatalog.Extra?.FirstOrDefault(x => x.Name == AddonConstants.ExtraName.Genre);
                var genres = extraGenres?.Options?.Select(o => o.ToString())?.ToList();
                var required = extraGenres?.IsRequired ?? false;

                var searchable = manifestCatalog.Extra?.Any(x => x.Name == AddonConstants.ExtraName.Search) ?? false;
                var skippable = manifestCatalog.Extra?.Any(x => x.Name == AddonConstants.ExtraName.Skip) ?? false;
                    
                if (genres != null)
                {
                    catalogs.Add(
                        new Catalog(
                            addon.Manifest.Id ?? "",
                            manifestCatalog.Type ?? "",
                            manifestCatalog.Id ?? "",
                            manifestCatalog.Name ?? "",
                            genres, 
                            required, 
                            searchable, 
                            skippable));
                }
            }

            return catalogs;

        }
    }
}

public class Catalog(
    string addonId,
    string type,
    string id, 
    string name,
    List<string> genres, 
    bool genreRequired,
    bool searchable,
    bool skippable)
{
    public string AddonId { get; } = addonId;
    public string Type { get; } = type;
    public string Id { get; } = id;
    public string Name { get; } = name;
    public List<string>? Genres { get; } = genres;
    public bool GenreRequired { get; } = genreRequired;
    public bool Searchable { get; } = searchable;
    public bool Skippable { get; } = skippable;
}