using AutoMapper;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Domain.Models.Portal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.Mappings
{
    public class ProductImageProfile: Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageResponseDto>();
            CreateMap<CreateProductImageDto, ProductImage>();
            CreateMap<UpdateProductImageDto, ProductImage>();
        }
    }
}
