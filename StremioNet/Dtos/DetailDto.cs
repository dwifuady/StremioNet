namespace StremioNet.Dtos;
using System.Text.Json.Serialization;

public class DetailDto
{
    [JsonPropertyName("meta")] public MetaDto? Meta { get; set; }

    [JsonIgnore] public string? AddonId { get; set; }
    [JsonIgnore] public string? AddonName { get; set; }
}
