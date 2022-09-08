using FluentValidation;

namespace BuberDinner.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(v => v.FirstName).NotEmpty().MaximumLength(200);
            RuleFor(v => v.LastName).NotEmpty().MaximumLength(200);
            RuleFor(v => v.Email).NotEmpty().MaximumLength(256).EmailAddress();
            RuleFor(v => v.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        }
    }
}