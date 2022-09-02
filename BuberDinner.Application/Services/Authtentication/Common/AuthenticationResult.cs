using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authtentication
{
    public record AuthenticationResult
    (
        User User,
        string Token
    );
}