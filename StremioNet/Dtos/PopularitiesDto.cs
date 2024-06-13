﻿namespace StremioNet.Dtos;

using System.Text.Json.Serialization;

public class PopularitiesDto
{
    [JsonPropertyName("moviedb")]
    public double? MovieDb { get; set; }

    [JsonPropertyName("stremio")]
    public double? Stremio { get; set; }

    [JsonPropertyName("stremio_lib")]
    public double? StremioLib { get; set; }

    [JsonPropertyName("trakt")]
    public int? Trakt { get; set; }
}