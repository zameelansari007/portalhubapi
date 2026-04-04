using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Portal
{
    public class CreateProductImageDto
    {
        public long ProductId { get; set; }

        public string ImageUrl { get; set; } = null!;

        public bool IsPrimary { get; set; } = false;

        public int DisplayOrder { get; set; } = 0;
    }

    public class ProductImageResponseDto
    {
        public long ProductImageId { get; set; }

        public long ProductId { get; set; }

        public string ImageUrl { get; set; } = null!;

        public bool IsPrimary { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class UpdateProductImageDto
    {
        public long ProductImageId { get; set; }

        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }

        public int DisplayOrder { get; set; }
    }
}
