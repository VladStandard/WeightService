using System.Net;

namespace Ws.Shared.Api.ApiException;

public class ApiExceptionServer : Exception
{
    public required string ErrorDisplayMessage { get; set; } = string.Empty;
    public string ErrorInternalMessage { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
}