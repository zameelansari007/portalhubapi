using FluentValidation;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Validators.Portal
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.SlugPath)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.IdPath)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Level)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.ParentId)
                .GreaterThan(0)
                .When(x => x.ParentId.HasValue);
        }
    }
}
