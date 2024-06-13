using System.Text.Json.Serialization;

namespace StremioNet.Dtos;

public class TrailerDto
{
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
