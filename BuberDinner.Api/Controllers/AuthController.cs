using BuberDinner.Application.Services.Authtentication;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthenticationService _authenticationSerive;

        public AuthController(IAuthenticationService authenticationSerive)
        {
            _authenticationSerive = authenticationSerive;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            // var registerResult = _authenticationSerive.Register(request.FirstName, request.LastName, request.Email, request.Password);

            // return registerResult.Match<IActionResult>(
            //     result => Ok(result),
            //     error => Problem(statusCode: (int) error.StatusCode, title: error.Message)
            // );

            ErrorOr<AuthenticationResult> registerResult = _authenticationSerive.Register(request.FirstName, request.LastName, request.Email, request.Password);

            return registerResult.Match<IActionResult>(
                result => Ok(MapAuthenticationResponse(result)),
                errors => Problem(errors)
            );
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            ErrorOr<AuthenticationResult> loginResult = _authenticationSerive.Login(request.Email, request.Password);

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