using AutoMapper;
using FluentValidation;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Portal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.Services
{
    

    public class ProductImageService :
    CrudService<
        ProductImage,
        CreateProductImageDto,
        UpdateProductImageDto,
        ProductImageResponseDto>
    {
        public ProductImageService(
            IQueryRepository<ProductImage> queryRepo,
            IRepository<ProductImage> commandRepo,
            IMapper mapper,
            IValidator<ProductImage> validator)
            : base(queryRepo, commandRepo, mapper, validator)
        {
        }
    }
}
