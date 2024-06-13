using Refit;
using StremioNet.Dtos.Manifest;

namespace StremioNet.Endpoints.Subtitle;

public class SubtitleAddons(
    AddonDto addon) : StremioAddons(addon)
{
    public ISubtitleApi Api { get; } = RestService.For<ISubtitleApi>(addon.BaseUri);
}