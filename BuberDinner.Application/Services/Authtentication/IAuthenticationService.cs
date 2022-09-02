using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
using ErrorOr;
using OneOf;

namespace BuberDinner.Application.Services.Authtentication
{
    public interface IAuthenticationService
    {
        // OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password);
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}