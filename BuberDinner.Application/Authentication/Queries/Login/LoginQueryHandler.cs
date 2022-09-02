using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authtentication;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(request.Email);
        var errors = new List<Error>();

        if (user == null)
        {
            // return Errors.Authentication.InvalidCredentials;
            return Error.Validation(code: "INVALID_CREDENTIALS", description: "Invalid credentials.");
        }

        if (user.Password != request.Password)
        {
            // return Errors.Authentication.InvalidCredentials;
            return Error.Validation(code: "INVALID_CREDENTIALS", description: "Invalid credentials.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}