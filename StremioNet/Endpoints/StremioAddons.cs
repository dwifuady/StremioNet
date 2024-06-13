using StremioNet.Common;
using StremioNet.Dtos.Manifest;

namespace StremioNet.Endpoints;

public abstract class StremioAddons(AddonDto addon)
{
    /// <summary>
    /// Supported Catalog Types. Movies, Series, etc
    /// </summary>
    public IEnumerable<CatalogTypeEnum> CatalogTypes { get; } = GetCatalogTypesEnum(addon.Manifest.Types);
    
    /// <summary>
    /// Addon ID
    /// </summary>
    public string Id { get; } = addon.Manifest.Id ?? string.Empty;

    /// <summary>
    /// Addon Description
    /// </summary>
    public string Description { get; } = addon.Manifest.Description ?? string.Empty;

    /// <summary>
    /// Addon Name
    /// </summary>
    public string Name { get; } = addon.Manifest.Name ?? string.Empty;

    /// <summary>
    /// Addon Version
    /// </summary>
    public string Version { get; } = addon.Manifest.Version ?? string.Empty;

    /// <summary>
    /// Check if provided type supported by this addon
    /// </summary>
    /// <param name="catalogType"></param>
    /// <returns></returns>
    public bool IsTypeSupported(CatalogTypeEnum catalogType)
    {
        return CatalogTypes.Any(ct => ct == catalogType);
    }

    private static IEnumerable<CatalogTypeEnum> GetCatalogTypesEnum(List<string>? catalogTypes)
    {
        if (catalogTypes is null)
        {
            return [];
        }

        var catalogTypeEnums = new List<CatalogTypeEnum>();


        foreach (var @type in catalogTypes)
        {
            if (AddonConstants.CatalogTypeDict.ContainsValue(@type))
            {
                catalogTypeEnums.Add(AddonConstants.CatalogTypeDict.FirstOrDefault(x => x.Value == @type).Key);
            }
        }

        return catalogTypeEnums;
    }
}