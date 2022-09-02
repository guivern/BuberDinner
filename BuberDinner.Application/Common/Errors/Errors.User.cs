using System.Net;
using ErrorOr;

namespace BuberDinner.Application.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(code: "DUPLICATE_EMAIL", description: "Email already exists.");
    }
}