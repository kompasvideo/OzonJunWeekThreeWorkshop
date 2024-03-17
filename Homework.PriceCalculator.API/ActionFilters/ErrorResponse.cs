using System.Net;

namespace Workshop.Api.ActionFilters;


public class ErrorResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public string Message { get; init; }        
    public ErrorResponse(HttpStatusCode StatusCode, string Message)
    {
        this.StatusCode = StatusCode;
        this.Message = Message;
    }
}