using BuberDinner.Application.Services.Authtentication;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQuery : IRequest<ErrorOr<AuthenticationResult>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}