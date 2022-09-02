using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authtentication;
using BuberDinner.Application.Services.Authtentication.Querys;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand { Email = request.Email, FirstName = request.FirstName, LastName = request.LastName, Password = request.Password };
            ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

            return registerResult.Match<IActionResult>(
                result => Ok(MapAuthenticationResponse(result)),
                errors => Problem(errors)
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery { Email = request.Email, Password = request.Password };
            ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);

            return loginResult.Match<IActionResult>(
                result => Ok(MapAuthenticationResponse(result)),
                errors => Problem(errors)
            );
        }

        private AuthenticationResponse MapAuthenticationResponse(AuthenticationResult result)
        {
            return new AuthenticationResponse(
                result.User.Id,
                result.User.FirstName,
                result.User.LastName,
                result.User.Email,
                result.Token
            );
        }
    }
}