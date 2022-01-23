using System.Net;

namespace TileGameClient.Infrastructure.Responses;

public class HttpStatusCodeResponse<TResponse>
{
    public TResponse Result { get; init; }

    public HttpStatusCode StatusCode { get; init; }
}