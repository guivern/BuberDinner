using ErrorOr;
using OneOf;

namespace BuberDinner.Application.Services.Authtentication
{
    public interface IAuthenticationCommandService
    {
        // OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password);
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    }
}