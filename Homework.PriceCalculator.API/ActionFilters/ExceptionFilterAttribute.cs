using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Workshop.Api.ActionFilters;

public partial class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException exception:
                HandleBadRequest(context, exception);
                return;
            default:
                HandleInternalError(context);
                return;
        }
    }

    private static void HandleInternalError(ExceptionContext context)
    {
        var jsonResult = new JsonResult(new ErrorResponse(
            HttpStatusCode.InternalServerError, 
            "Возникла ошибка, уже чиним"));
        jsonResult.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = jsonResult;
    }

    private static void HandleBadRequest(ExceptionContext context, ValidationException exception)
    {
        var jsonResult = new JsonResult(new ErrorResponse(
            HttpStatusCode.BadRequest, 
            exception.Message));
        jsonResult.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = jsonResult;
    }
}