using FluentValidation;
using WebAPI.Application.DTOs;

namespace WebAPI.Application.Validators;
public class UserDtoValidator : AbstractValidator<RegisterRequest>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(5, 20).WithMessage("Username must be between 5 and 20 characters.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}

