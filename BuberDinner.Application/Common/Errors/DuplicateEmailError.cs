using System.Net;
namespace BuberDinner.Application.Common.Errors;

public class DuplicateEmailError: IError
{
    public string Message { get; set; } = "User with given email already exists.";
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
}