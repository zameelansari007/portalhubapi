using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PortalHub.Application.DTOs.Catalog;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;

namespace PortalHub.Infrastructure.Dapper
{
    public class CatalogQueryRepository : ICatalogQueryRepository
    {
        private readonly string _connectionString;

        public CatalogQueryRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection missing");
        }

        private SqlConnection CreateConnection()
            => new SqlConnection(_connectionString);


        public async Task<IEnumerable<CategoryMenuDto>> GetCategoriesAsync(long supplierId)
        {
            var sql = @"

    SELECT 
    c.CategoryId,
    c.Name,
    c.Slug,
    c.ParentId,
    c.Level
FROM portal.Categories c
WHERE 
    c.IsActive = 1
    AND (
        @SupplierId IS NULL
        OR EXISTS (
            SELECT 1
            FROM portal.Products p
            WHERE p.CategoryId = c.CategoryId
            AND p.SupplierId = @SupplierId
            AND p.IsActive = 1
        )
    )
ORDER BY c.SortOrder";

            using var conn = CreateConnection();

            return await conn.QueryAsync<CategoryMenuDto>(sql, new { SupplierId = supplierId });
        }

        public async Task<PagedResultDto<ProductListDto>> GetProductsAsync(ProductListRequestDto request)
        {
            var offset = (request.PageNumber - 1) * request.PageSize;

            var sql = @"

    SELECT COUNT(*)
FROM portal.Products p
JOIN portal.ProductVariants v ON v.ProductId = p.ProductId
WHERE 
    p.IsActive = 1
    AND v.IsActive = 1
    AND (@SupplierId IS NULL OR p.SupplierId = @SupplierId)
    AND (@CategoryId IS NULL OR p.CategoryId = @CategoryId)


SELECT 
    p.ProductId,
    p.CategoryId,
    p.Name,
    p.Slug,
    v.Price,
    pi.ImageUrl
FROM portal.Products p
JOIN portal.ProductVariants v ON v.ProductId = p.ProductId
LEFT JOIN portal.ProductImages pi
    ON pi.ProductId = p.ProductId AND pi.IsPrimary = 1
WHERE 
    p.IsActive = 1
    AND v.IsActive = 1
    AND (@SupplierId IS NULL OR p.SupplierId = @SupplierId)
    AND (@CategoryId IS NULL OR p.CategoryId = @CategoryId)
ORDER BY p.ProductId
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY";

            using var conn = CreateConnection();

            using var multi = await conn.QueryMultipleAsync(sql, new
            {
                request.SupplierId,
                request.CategoryId,
                Offset = offset,
                request.PageSize
            });

            var total = await multi.ReadSingleAsync<int>();

            var items = (await multi.ReadAsync<ProductListDto>()).ToList();

            return new PagedResultDto<ProductListDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = total,
                TotalPages = (int)Math.Ceiling((double)total / request.PageSize),
                Items = items
            };
        }

    }

}
