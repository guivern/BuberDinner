using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authtentication;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

            return registerResult.Match<IActionResult>(
                result => Ok(MapAuthenticationResponse(result)),
                errors => Problem(errors)
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
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