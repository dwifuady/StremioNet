using System.Text.Json.Serialization;
namespace StremioNet.Dtos;

public class SubtitleDto
{
    [JsonPropertyName("subtitles")]
    public List<Subtitle>? Subtitles { get; set; }
    [JsonIgnore] public string? AddonId { get; set; }
    [JsonIgnore] public string? AddonName { get; set; }
}

public class Subtitle
{
    [JsonPropertyName("lang")]
    public string? Lang { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("SubEncoding")]
    public string? SubEncoding { get; set; }
}