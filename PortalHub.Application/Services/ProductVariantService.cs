using AutoMapper;
using FluentValidation;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Portal;

public class ProductVariantService :
    CrudService<
        ProductVariant,
        CreateProductVariantDto,
        UpdateProductVariantDto,
        ProductVariantResponseDto>
{
    public ProductVariantService(
        IQueryRepository<ProductVariant> queryRepo,
        IRepository<ProductVariant> commandRepo,
        IMapper mapper,
        IValidator<ProductVariant> validator)
        : base(queryRepo, commandRepo, mapper, validator)
    {
    }
}
