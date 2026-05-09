using FluentValidation;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Validators.Master
{
    public class StateValidator : AbstractValidator<State>
    {
        public StateValidator()
        {
            RuleFor(x => x.CountryId)
                .GreaterThan(0);

            RuleFor(x => x.StateName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}