using ErrorOr;
using OneOf;

namespace BuberDinner.Application.Services.Authtentication.Querys
{
    public interface IAuthenticationQueryService
    {
        // OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password);
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}