using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using ErrorOr;
using OneOf;

namespace BuberDinner.Application.Services.Authtentication
{
    public class AuthtenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthtenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
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

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // check if user already exits
            if (_userRepository.GetUserByEmail(email) != null)
            {
                return Errors.User.DuplicateEmail;
            }

            // create user
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);

            // generate jwt token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}