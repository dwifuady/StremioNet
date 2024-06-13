using Refit;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Subtitle;

public interface ISubtitleApi
{
    [Get("/subtitles/{type}/{id}.json")]
    Task<SubtitleDto> GetSubtitles(string type, string id);
}