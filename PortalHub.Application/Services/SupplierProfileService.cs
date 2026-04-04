using AutoMapper;
using FluentValidation;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Services
{
    public class SupplierProfileService :
    CrudService<
        SupplierProfile,
        CreateSupplierProfileDto,
        UpdateSupplierProfileDto,
        SupplierProfileResponseDto>
    {
        public SupplierProfileService(
            IQueryRepository<SupplierProfile> queryRepo,
            IRepository<SupplierProfile> commandRepo,
            IMapper mapper,
            IValidator<SupplierProfile> validator)
            : base(queryRepo, commandRepo, mapper, validator)
        {
        }
    }
}
