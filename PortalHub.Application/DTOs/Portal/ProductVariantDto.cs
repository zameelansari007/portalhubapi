using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Portal
{
    /*
    public class CreateProductVariantDto
    {
        public long ProductId { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? CompareAtPrice { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateProductVariantDto
    {
        public long ProductVariantId { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? CompareAtPrice { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }
    }

    public class ProductVariantResponseDto
    {
        public long ProductVariantId { get; set; }

        public long ProductId { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? CompareAtPrice { get; set; }

        public int StockQuantity { get; set; }

        public int StockReserved { get; set; }

        public bool IsActive { get; set; }
    }

    */

    public class CreateProductVariantDto
    {
        public long ProductId { get; set; }

        public string? Color { get; set; }

        public string? Size { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal CompareAtPrice { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateProductVariantDto
    {
        public long ProductVariantId { get; set; }

        public string? Color { get; set; }

        public string? Size { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal CompareAtPrice { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }
    }

    public class ProductVariantResponseDto
    {
        public long ProductVariantId { get; set; }

        public long ProductId { get; set; }

        public string? Color { get; set; }

        public string? Size { get; set; }

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal CompareAtPrice { get; set; }

        public int StockQuantity { get; set; }

        public int StockReserved { get; set; }
        public int AvailableStock => StockQuantity - StockReserved;

        public bool IsActive { get; set; }
    }

}
