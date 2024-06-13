using StremioNet.Common;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Subtitle;

public interface ISubtitleEndpoint : IEndpointBase<SubtitleDto>
{

}

public class SubtitleEndpoint : EndpointBase, ISubtitleEndpoint
{
    internal SubtitleEndpoint(Stremio stremio) : base(stremio)
    {
    }

    public async Task<IEnumerable<SubtitleDto>> GetResultsAsync(CatalogTypeEnum type, string id)
    {
        List<SubtitleDto> results = [];

        foreach (var addon in SubtitleAddons.Where(m => m.IsTypeSupported(type)))
        {
            try
            {
                var result = await addon.Api.GetSubtitles(AddonConstants.CatalogTypeDict[type], id);
                result.AddonId = addon.Id;
                result.AddonName = addon.Name;
                if (result?.Subtitles?.Count > 0)
                {
                    results.Add(result);
                }
            }
            catch
            {
                // ignored
            }
        }

        return results;
    }

    public async IAsyncEnumerable<SubtitleDto> StreamResultsAsync(CatalogTypeEnum type, string id)
    {
        foreach (var addon in SubtitleAddons.Where(m => m.IsTypeSupported(type)))
        {
            SubtitleDto? result = null;
            try
            {
                result = await addon.Api.GetSubtitles(AddonConstants.CatalogTypeDict[type], id);
                result.AddonId = addon.Id;
                result.AddonName = addon.Name;
            }
            catch
            {
                // ignored
            }

            if (result?.Subtitles?.Count > 0)
            {
                yield return result;
            }

        }
    }
}