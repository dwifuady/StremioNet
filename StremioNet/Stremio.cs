using StremioNet.Dtos.Manifest;
using StremioNet.Endpoints.Catalog;
using StremioNet.Endpoints.Meta;
using StremioNet.Endpoints.Stream;
using StremioNet.Endpoints.Subtitle;

namespace StremioNet;

public class Stremio
{
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public Stremio(List<AddonDto> addons)
    {
        Addons = addons;
        Meta = new MetaEndpoint(this);
        Catalog = new CatalogEndpoint(this);
        Stream = new StreamEndpoint(this);
        Subtitle = new SubtitleEndpoint(this);
    }

    public Stremio(List<string> addonManifestUrls)
    {
        AddonManifestUrls = addonManifestUrls;
        Meta = new MetaEndpoint(this);
        Catalog = new CatalogEndpoint(this);
        Stream = new StreamEndpoint(this);
        Subtitle = new SubtitleEndpoint(this);
    }

    /// <summary>
    /// List of installed addons
    /// </summary>
    public List<AddonDto>? Addons { get; set; }
    public List<string>? AddonManifestUrls { get; set; }

    public IMetaEndpoint Meta { get; }
    public ICatalogEndpoint Catalog { get; }
    public IStreamEndpoint Stream { get; }
    public ISubtitleEndpoint Subtitle { get; }
}