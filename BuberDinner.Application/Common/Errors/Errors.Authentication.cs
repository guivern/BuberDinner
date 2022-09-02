using ErrorOr;

namespace BuberDinner.Application.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(code: "INVALID_CREDENTIALS", description: "Invalid credentials.");
    }
}