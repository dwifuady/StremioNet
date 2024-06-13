using StremioNet.Common;

namespace StremioNet.Endpoints.Catalog;

public class SearchRequest
{
    public SearchRequest(string type, string query)
    {
        if (Enum.TryParse(type, out CatalogTypeEnum typeEnum))
        {
            TypeEnum = typeEnum;
        }

        Query = query;
        Type = type;
    }

    public SearchRequest(CatalogTypeEnum type, string query)
    {
        TypeEnum = type;
        Type = AddonConstants.CatalogTypeDict[type];
        Query = query;
    }

    public CatalogTypeEnum TypeEnum { get; set; }
    public string Type { get; set; }
    public string Query { get; set; }
}