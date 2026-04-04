using AutoMapper;
using FluentValidation;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Portal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.Services
{
    //public class ProductService :
    //CrudService<
    //    Product,
    //    CreateProductDto,
    //    UpdateProductDto,
    //    ProductResponseDto>
    //{
    //    public ProductService(
    //        IQueryRepository<Product> queryRepo,
    //        IRepository<Product> commandRepo,
    //        IMapper mapper,
    //        IValidator<Product> validator)
    //        : base(queryRepo, commandRepo, mapper, validator)
    //    {
    //    }
    //}

    public class ProductService :
    CrudService<Product, CreateProductDto, UpdateProductDto, ProductResponseDto>
    {
        public ProductService(
            IQueryRepository<Product> queryRepo,
            IRepository<Product> commandRepo,
            IMapper mapper,
            IValidator<Product> validator)
            : base(queryRepo, commandRepo, mapper, validator)
        {
        }

        public override async Task<ServiceResult<ProductResponseDto>> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            // Generate slug automatically
            entity.Slug = GenerateSlug(entity.Name);

            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(entity);
                if (!result.IsValid)
                    return ServiceResult<ProductResponseDto>.Fail(
                        result.Errors.First().ErrorMessage,
                        ErrorCodes.ValidationError);
            }

            await _commandRepo.AddAsync(entity);
            await _commandRepo.SaveChangesAsync();

            return ServiceResult<ProductResponseDto>.Ok(
                _mapper.Map<ProductResponseDto>(entity));
        }

        private string GenerateSlug(string phrase)
        {
            string str = phrase.ToLower().Trim();

            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Replace(" ", "-");

            return str;
        }
    }

}
