using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Portal
{
    [Table("Products", Schema = "Portal")]
    public class Product
    {
        [Key]
        public long ProductId { get; set; }

        [Required]
        public long SupplierId { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Slug { get; set; } = null!;   // ✅ Added

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        // 🔹 Navigation
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(SupplierId))]
        public User Supplier { get; set; } = null!;

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
