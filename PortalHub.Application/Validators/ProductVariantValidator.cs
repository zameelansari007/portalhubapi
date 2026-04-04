using FluentValidation;
using PortalHub.Domain.Models.Portal;

public class ProductVariantValidator : AbstractValidator<ProductVariant>
{
    public ProductVariantValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Color)
            .MaximumLength(50);

        RuleFor(x => x.Size)
            .MaximumLength(50);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.CompareAtPrice)
            .GreaterThan(x => x.Price)
            .WithMessage("CompareAtPrice must be greater than Price.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.StockReserved)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x)
            .Must(x => x.StockReserved <= x.StockQuantity)
            .WithMessage("Reserved stock cannot exceed total stock.");
    }
}
