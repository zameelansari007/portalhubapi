using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.Interfaces.Queries
{
    public interface ICatalogQueryRepository
    {
        //Task<IEnumerable<CategoryMenuDto>> GetCategoriesAsync(long supplierId);

        //Task<PagedResultDto<ProductListDto>> GetProductsAsync(ProductListRequestDto request);
        Task<ServiceResult<IEnumerable<CategoryMenuDto>>> GetCategoriesAsync(long supplierId);

        Task<ServiceResult<PagedResultDto<ProductListDto>>> GetProductsAsync(ProductListRequestDto request);

    }
}
