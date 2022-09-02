using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using ErrorOr;
using OneOf;

namespace BuberDinner.Application.Services.Authtentication.Querys
{
    public class AuthtenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthtenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            var errors = new List<Error>();

            if (user == null)
            {
                // return Errors.Authentication.InvalidCredentials;
                return Error.Validation(code: "INVALID_CREDENTIALS", description: "Invalid credentials.");
            }

            if (user.Password != password)
            {
                // return Errors.Authentication.InvalidCredentials;
                return Error.Validation(code: "INVALID_CREDENTIALS", description: "Invalid credentials.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}