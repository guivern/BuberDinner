using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authtentication
{
    public class AuthtenticationService : IAuthenticationService
    {
        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(), "firstName", "lastName", email, "token");
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(), firstName, lastName, email, "token");
        }
    }
}