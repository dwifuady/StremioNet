namespace StremioNet.Dtos;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DiscoverDto
{
    [JsonPropertyName("metas")]
    public List<MetaDto>? Metas { get; set; }

    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    [JsonPropertyName("cacheMaxAge")]
    public int CacheMaxAge { get; set; }

    [JsonPropertyName("staleRevalidate")]
    public int StaleRevalidate { get; set; }

    [JsonPropertyName("staleError")]
    public int StaleError { get; set; }

    [JsonIgnore] public string? Type { get; set; }
    [JsonIgnore] public string? CatalogName { get; set; }
    [JsonIgnore] public string? AddonId { get; set; }
    [JsonIgnore] public string? AddonName { get; set; }
    [JsonIgnore] public string? Genre { get; set; }
}