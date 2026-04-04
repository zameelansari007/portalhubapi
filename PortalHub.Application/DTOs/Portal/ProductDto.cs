using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Portal
{
    public class CreateProductDto
    {
        public long CategoryId { get; set; }
        public long SupplierId { get; set; }

        public string Name { get; set; } = null!;
        //public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }

    public class UpdateProductDto
    {
        public long ProductId { get; set; }

        public long CategoryId { get; set; }

        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }
    }

    public class ProductResponseDto
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public long SupplierId { get; set; }

        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
