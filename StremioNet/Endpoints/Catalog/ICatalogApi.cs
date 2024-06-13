using Refit;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Catalog;

public interface ICatalogApi
{
    [Get("/catalog/{type}/{id}.json")]
    Task<DiscoverDto> GetCatalogByCatalogId(string type, string id);

    [Get("/catalog/{type}/{id}/genre={genre}.json")]
    Task<DiscoverDto> GetCatalogByGenre(string type, string id, string genre);

    [Get("/catalog/{type}/{id}/search={query}.json")]
    Task<DiscoverDto> SearchFromCatalog(string type, string id, string query);
}