// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using StremioNet;
using StremioNet.Common;
using StremioNet.Endpoints.Catalog;

var urls = new List<string>
{
    "https://v3-cinemeta.strem.io/manifest.json",
    "https://watchhub.strem.io/manifest.json",
    "https://thepiratebay-plus.strem.fun/manifest.json",
    "https://opensubtitles-v3.strem.io/manifest.json"
};

var api = new Stremio(urls);

await foreach (var result in api.Meta.StreamResultsAsync(CatalogTypeEnum.Movie, "tt15239678"))
{
    Console.WriteLine($"Getting result from Addon [{result.AddonId}] {result.AddonName}");
    Console.WriteLine($"{result.Meta?.Name} {result.Meta?.Year}");
    Console.WriteLine(result.Meta?.Description);
}
/*
*/

/*
await foreach (var result in api.Subtitle.StreamResultsAsync(CatalogTypeEnum.Movie, "tt15239678"))
{
    Console.WriteLine($"{DateTime.Now} : Received result from [{result.AddonId}] {result.AddonName}");

    if (result?.Subtitles != null)
    {
        foreach (var subtitle in result.Subtitles)
        {
            Console.WriteLine($"{subtitle.Id} | {subtitle.Lang} | {subtitle.Url}");
        }
    }
}
*/

/*
await foreach (var result in api.Stream.StreamResultsAsync(CatalogTypeEnum.Movie, "tt15239678"))
{
    Console.WriteLine($"{DateTime.Now} : Received result from [{result.AddonId}] {result.AddonName}");

    if (result?.Streams != null)
    {
        foreach (var stream in result.Streams)
        {
            Console.WriteLine($"{stream.Name} | {stream.Title} | {(stream.Url ?? stream.ExternalUrl) ?? stream.InfoHash}");
        }
    }
}
*/

/*
var catalogs = await api.Catalog.GetCatalogs();

var catalogString = JsonSerializer.Serialize(catalogs);
Console.WriteLine(catalogString);
*/

/*
var catalogRequests = new List<CatalogRequest>
{
    new(CatalogTypeEnum.Movie, "top", ""),
    new(CatalogTypeEnum.Series, "top", ""),
    new(CatalogTypeEnum.Movie, "top", "Animation"),
};

var addonCatalogRequest = new AddonCatalogRequest("com.linvo.cinemeta", catalogRequests.ToArray());
var addonCatalogRequests = new List<AddonCatalogRequest> { addonCatalogRequest };
var getDiscoverRequest = new GetDiscoverRequest(addonCatalogRequests.ToArray());

await foreach (var result in api.Catalog.StreamDiscoverAsync(getDiscoverRequest))
{
    Console.WriteLine($"{DateTime.Now} : Received result from [{result.AddonId}] {result.AddonName}");

    if (result.Metas != null)
    {
        Console.WriteLine($"{result.Type} - {result.CatalogName} {(!string.IsNullOrEmpty(result.Genre) ? $" - {result.Genre}" : "")}");
        foreach (var resultMeta in result.Metas)
        {
            Console.WriteLine($"{resultMeta.Name} ({resultMeta.Year})");
        }
    }
}
*/

/*
var searchRequest = new SearchRequest(CatalogTypeEnum.Movie, "Fall Guy");
await foreach (var result in api.Catalog.StreamSearchAsync(searchRequest))
{
    Console.WriteLine($"{DateTime.Now} : Received result from [{result.AddonId}] {result.AddonName}");

    if (result.Metas != null)
    {
        Console.WriteLine($"{result.Type} - {result.CatalogName} {(!string.IsNullOrEmpty(result.Genre) ? $" - {result.Genre}" : "")}");
        foreach (var resultMeta in result.Metas)
        {
            Console.WriteLine($"{resultMeta.Name} ({resultMeta.Year})");
        }
    }
}
*/