using Refit;
using StremioNet.Dtos.Manifest;

namespace StremioNet.Endpoints.Stream;

public class StreamAddons(
    AddonDto addon) : StremioAddons(addon)
{
    public IStreamApi Api { get; } = RestService.For<IStreamApi>(addon.BaseUri);
}