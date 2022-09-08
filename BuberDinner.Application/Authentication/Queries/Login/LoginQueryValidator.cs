using FluentValidation;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(v => v.Email).NotEmpty().MaximumLength(256).EmailAddress();
            RuleFor(v => v.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        }
    }
}