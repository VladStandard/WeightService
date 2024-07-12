using System.Net;

namespace Ws.Shared.Api.ApiException;

public class ApiExceptionServer : Exception
{
    public required Enum ExceptionType { get; set; }
    public string ErrorDisplayMessage { get; set; } = string.Empty;
    public string ErrorInternalMessage { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
}