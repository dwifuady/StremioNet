using Refit;
using StremioNet.Dtos.Manifest;

namespace StremioNet;

public interface IStremioApi
{
    [Get("/addonscollection.json")]
    Task<List<AddonDto>>? GetAddonCollection();
}