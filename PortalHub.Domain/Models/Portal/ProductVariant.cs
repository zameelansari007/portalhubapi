using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Portal
{
    [Table("ProductVariants", Schema = "Portal")]
   
    public class ProductVariant
    {
        [Key]
        public long ProductVariantId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [MaxLength(50)]
        public string? Color { get; set; }

        [MaxLength(50)]
        public string? Size { get; set; }

        [Required]
        [MaxLength(100)]
        public string Sku { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CompareAtPrice { get; set; }

        public int StockQuantity { get; set; } = 0;

        public int StockReserved { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // 🔹 Navigation
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}
