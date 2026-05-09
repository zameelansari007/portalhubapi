using FluentValidation;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Validators.Master
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}