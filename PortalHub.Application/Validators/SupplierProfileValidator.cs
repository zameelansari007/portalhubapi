using FluentValidation;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Domain.Models.Master;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Validators.Portal
{
    public class SupplierProfileValidator
        : AbstractValidator<SupplierProfile>//AbstractValidator<CreateSupplierProfileDto>
    {
        public SupplierProfileValidator()
        {
            RuleFor(x => x.SupplierId).GreaterThan(0);

            RuleFor(x => x.BusinessName)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.OfficeAddressLine1)
                .NotEmpty();

            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
            RuleFor(x => x.Pincode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
        }
    }
}
