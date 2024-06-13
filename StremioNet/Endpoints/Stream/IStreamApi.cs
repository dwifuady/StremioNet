using Refit;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Stream;

public interface IStreamApi
{
    [Get("/stream/{type}/{id}.json")]
    Task<StreamResponseDto> GetStream(string type, string id);
}