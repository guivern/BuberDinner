using System.Net;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{

    protected IActionResult Problem(List<Error> errors)
    {
        // validation error
        if (errors.All(x => x.Type == ErrorType.Validation))
        {
            ModelStateDictionary modelState = new();

            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelState);
        }

        // internal server error
        if (errors.Any(x => x.Type == ErrorType.Unexpected))
        {
            return Problem();
        }

        // other errors
        var firstError = errors.First();
        var statusCode = firstError.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }

}