using StremioNet.Common;

namespace StremioNet.Endpoints.Catalog;

public record GetDiscoverRequest(AddonCatalogRequest[] AddonCatalogRequests);

public record AddonCatalogRequest(string AddonId, CatalogRequest[] CatalogGenres);
public class CatalogRequest
{
    public CatalogRequest(string type, string catalogId)
    {
        Type = type;
        CatalogId = catalogId;

        if (Enum.TryParse(type, out CatalogTypeEnum typeEnum))
        {
            TypeEnum = typeEnum;
        }
    }

    public CatalogRequest(string type, string catalogId, string genres)
    {
        Type = type;
        CatalogId = catalogId;
        Genres = genres;

        if (Enum.TryParse(type, out CatalogTypeEnum typeEnum))
        {
            TypeEnum = typeEnum;
        }
    }

    public CatalogRequest(CatalogTypeEnum type, string catalogId, string genres)
    {
        Type = AddonConstants.CatalogTypeDict[type];
        TypeEnum = type;
        CatalogId = catalogId;
        Genres = genres;
    }

    public CatalogTypeEnum TypeEnum { get; set; }
    public string Type { get; init; }
    public string CatalogId { get; init; }
    public string? Genres { get; init; }
}