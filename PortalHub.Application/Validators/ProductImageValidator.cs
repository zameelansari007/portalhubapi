using FluentValidation;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Validators
{
   

    public class ProductImageValidator : AbstractValidator<ProductImage>
    {
        public ProductImageValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("ImageUrl is required and cannot exceed 500 characters.");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DisplayOrder cannot be negative.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty()
                .WithMessage("CreatedAt is required.");
        }
    }
}
