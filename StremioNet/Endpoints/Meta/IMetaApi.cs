using Refit;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Meta;

public interface IMetaApi
{
    [Get("/meta/{type}/{id}.json")]
    Task<DetailDto> GetMeta(string type, string id);
}