using StremioNet.Dtos.Manifest;
using System.Text.Json;
using StremioNet.Endpoints.Catalog;
using StremioNet.Endpoints.Meta;
using StremioNet.Endpoints.Stream;
using StremioNet.Endpoints.Subtitle;

namespace StremioNet.Endpoints;

public abstract class EndpointBase
{
    protected readonly Stremio Stremio;
    
    protected readonly List<MetaAddons> MetaAddons = [];
    protected readonly List<CatalogAddons> CatalogAddons = [];
    protected readonly List<StreamAddons> StreamAddons = [];
    protected readonly List<SubtitleAddons> SubtitleAddons = [];
    internal EndpointBase(
        Stremio stremio)
    {
        Stremio = stremio;
        InitializeAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeAsync()
    {
        var client = GetClient();
        await DownloadAddonManifests(client);
        GenerateApis();
    }

    protected HttpClient GetClient()
    {
        var clientFactory = Stremio.HttpClientFactory;

        var client = clientFactory != null ?
            clientFactory.CreateClient() :
            new HttpClient();

        return client;
    }

    /// <summary>
    /// Download Addon Manifests if the addon url is provided
    /// </summary>
    /// <returns></returns>
    private async Task DownloadAddonManifests(HttpClient httpClient)
    {
        if (Stremio.AddonManifestUrls is null || Stremio.Addons is { Count: > 0 })
            return;

        var newAddons = new List<AddonDto>(); // Temporary list to hold new addons

        foreach (var url in Stremio.AddonManifestUrls.Distinct())
        {
            if (newAddons.Any(a => a.TransportUrl == url))
                continue;

            var manifestString = await httpClient.GetStringAsync(url);
            var manifest = JsonSerializer.Deserialize<ManifestDto>(manifestString);

            if (manifest == null)
                continue;

            newAddons.Add(new AddonDto
            {
                Manifest = manifest,
                TransportUrl = url
            });
        }

        Stremio.Addons = newAddons;
    }

    private void GenerateApis()
    {
        if (Stremio.Addons is { Count: <= 0 })
            return;

        GenerateMetaApis();
        GenerateCatalogApis();
        GenerateStreamApis();
        GenerateSubtitleApis();
    }

    private void GenerateMetaApis()
    {
        foreach (var addon in Stremio.Addons!.Where(a => a.Manifest.IsMetaAddonSupported))
        {
            MetaAddons.Add(new MetaAddons(addon));
        }
    }

    private void GenerateCatalogApis()
    {
        foreach (var addon in Stremio.Addons!.Where(a => a.Manifest.IsCatalogAddonSupported))
        {
            CatalogAddons.Add(new CatalogAddons(addon));
        }
    }

    private void GenerateStreamApis()
    {
        foreach (var addon in Stremio.Addons!.Where(a => a.Manifest.IsStreamAddonSupported))
        {
            StreamAddons.Add(new StreamAddons(addon));
        }
    }

    private void GenerateSubtitleApis()
    {
        foreach (var addon in Stremio.Addons!.Where(a => a.Manifest.IsSubtitleAddonSupported))
        {
            SubtitleAddons.Add(new SubtitleAddons(addon));
        }
    }
}