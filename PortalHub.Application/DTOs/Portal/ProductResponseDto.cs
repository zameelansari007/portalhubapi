using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Portal
{
    public class ProductResponseDto2
    {
        public string Id { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public long Category { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public int Sold { get; set; }
        public string Delivery { get; set; } = string.Empty;
        public bool VideoAvailable { get; set; }
    }
}
