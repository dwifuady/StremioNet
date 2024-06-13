using Refit;
using StremioNet.Dtos.Manifest;

namespace StremioNet.Endpoints.Meta;

public class MetaAddons(
    AddonDto addon) : StremioAddons(addon)
{
    public IMetaApi Api { get; } = RestService.For<IMetaApi>(addon.BaseUri);
}