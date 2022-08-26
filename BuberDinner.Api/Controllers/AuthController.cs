using BuberDinner.Application.Services.Authtentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationSerive;

        public AuthController(IAuthenticationService authenticationSerive)
        {
            _authenticationSerive = authenticationSerive;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var registerResult = _authenticationSerive.Register(request.FirstName, request.LastName, request.Email, request.Password);

            var response = new AuthenticationResponse(registerResult.Id, request.FirstName, request.LastName, request.Email, registerResult.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var loginResult = _authenticationSerive.Login(request.Email, request.Password);

            var response = new AuthenticationResponse(loginResult.Id, loginResult.FirstName, loginResult.LastName, loginResult.Email, loginResult.Token);

            return Ok(response);
        }
    }
}