﻿namespace StremioNet.Dtos.Manifest;

using System.Text.Json.Serialization;

public class CatalogDto
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("genres")]
    public List<object>? Genres { get; set; }

    [JsonPropertyName("extra")]
    public List<ExtraDto>? Extra { get; set; }

    [JsonPropertyName("extraSupported")]
    public List<string>? ExtraSupported { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("extraRequired")]
    public List<string>? ExtraRequired { get; set; }

    [JsonPropertyName("pageSize")]
    public int? PageSize { get; set; }
}