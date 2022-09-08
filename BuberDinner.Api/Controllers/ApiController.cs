using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDinner.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        // validation error
        if (errors.All(x => x.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        // internal server error
        if (errors.Any(x => x.Type == ErrorType.Unexpected))
        {
            return Problem();
        }

        // other errors
        return Problem(errors.First());
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        ModelStateDictionary modelState = new();

        foreach (var error in errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modelState);
    }

    private IActionResult Problem(Error error)
    {
        if (error.NumericType == 401)
        {
            return Unauthorized(error);
        }

        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }
}