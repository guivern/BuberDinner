using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (exception is ArgumentException) code = HttpStatusCode.BadRequest;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new ObjectResult(new { error = exception.Message });

            context.ExceptionHandled = true;
        }
    }
}