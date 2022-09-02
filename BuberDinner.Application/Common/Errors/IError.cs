using System.Net;
namespace BuberDinner.Application.Common.Errors;

public interface IError
{
    string Message { get; set; }
    HttpStatusCode StatusCode { get; set; }
}