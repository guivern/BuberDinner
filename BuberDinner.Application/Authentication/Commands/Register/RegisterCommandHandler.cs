namespace BuberDinner.Application.Authentication.Commands.Register
{
    using System.Threading;
    using System.Threading.Tasks;
    using BuberDinner.Application.Common.Interfaces.Authentication;
    using BuberDinner.Application.Common.Interfaces.Persistence;
    using BuberDinner.Application.Services.Authtentication;
    using BuberDinner.Domain.Entities;
    using ErrorOr;
    using MediatR;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            
            if (_userRepository.GetUserByEmail(command.Email) != null)
            {
                return Error.Conflict(code: "DUPLICATE_EMAIL", description: "Email already exists.");
            }

            // create user
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            _userRepository.Add(user);

            // generate jwt token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}