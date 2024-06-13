using StremioNet.Common;

namespace StremioNet.Endpoints;

public interface IEndpointBase<T>
{
    Task<IEnumerable<T>> GetResultsAsync(CatalogTypeEnum type, string id);
    IAsyncEnumerable<T> StreamResultsAsync(CatalogTypeEnum type, string id);
}