using PortalHub.Application.DTOs.Portal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Catalog
{
    public class CategoryMenuDto
    {
        public long CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public long? ParentId { get; set; }

        public int Level { get; set; }
    }
    public class ProductListDto
    {
        public long ProductId { get; set; }

        public long CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
    public class ProductListRequestDto
    {
        public long? SupplierId { get; set; }

        public long? CategoryId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
    public class PagedResultDto<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public List<T> Items { get; set; } = new();
    }
}
