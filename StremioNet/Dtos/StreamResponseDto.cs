namespace StremioNet.Dtos;

using System.Text.Json.Serialization;

public class StreamResponseDto
{
    [JsonPropertyName("streams")]
    public List<StreamDto>? Streams { get; set; }

    [JsonPropertyName("cacheMaxAge")]
    public int CacheMaxAge { get; set; }

    [JsonPropertyName("staleRevalidate")]
    public int StaleRevalidate { get; set; }

    [JsonPropertyName("staleError")]
    public int StaleError { get; set; }

    [JsonIgnore] public string? AddonId { get; set; }
    [JsonIgnore] public string? AddonName { get; set; }
}