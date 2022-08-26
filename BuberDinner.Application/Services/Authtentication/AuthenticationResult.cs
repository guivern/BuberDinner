namespace BuberDinner.Application.Services.Authtentication
{
    public record AuthenticationResult
    (
        Guid Id,
        string FirstName,
        string LastName,
        string Email, 
        string Token
    );
}