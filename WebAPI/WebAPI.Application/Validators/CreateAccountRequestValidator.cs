using FluentValidation;
using WebAPI.Application.DTOs;
using WebAPI.Domain.Constants;

namespace WebAPI.Application.Validators
{
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountRequestValidator()
        {
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Must(currency => CurrencyConstants.AllowedCurrencies.Contains(currency))
                .WithMessage("Currency must be one of the following values: USD, UAH, EUR.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
