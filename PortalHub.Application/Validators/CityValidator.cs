using FluentValidation;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Validators.Master
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(x => x.StateId)
                .GreaterThan(0);

            RuleFor(x => x.CityName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}