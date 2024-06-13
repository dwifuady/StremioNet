using StremioNet.Common;
using StremioNet.Dtos;

namespace StremioNet.Endpoints.Stream;

public interface IStreamEndpoint : IEndpointBase<StreamResponseDto>
{

}

public class StreamEndpoint : EndpointBase, IStreamEndpoint
{
    internal StreamEndpoint(Stremio stremio) : base(stremio)
    {
    }
    public async Task<IEnumerable<StreamResponseDto>> GetResultsAsync(CatalogTypeEnum type, string id)
    {
        List<StreamResponseDto> results = [];

        foreach (var addon in StreamAddons.Where(m => m.IsTypeSupported(type)))
        {
            try
            {
                var result = await addon.Api.GetStream(AddonConstants.CatalogTypeDict[type], id);
                result.AddonId = addon.Id;
                result.AddonName = addon.Name;
                if (result?.Streams?.Count > 0)
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

    public async IAsyncEnumerable<StreamResponseDto> StreamResultsAsync(CatalogTypeEnum type, string id)
    {
        foreach (var addon in StreamAddons.Where(m => m.IsTypeSupported(type)))
        {
            StreamResponseDto? result = null;
            try
            {
                result = await addon.Api.GetStream(AddonConstants.CatalogTypeDict[type], id);
                result.AddonId = addon.Id;
                result.AddonName = addon.Name;
            }
            catch
            {
                // ignored
            }

            if (result?.Streams?.Count > 0)
            {
                yield return result;
            }

        }
    }

}