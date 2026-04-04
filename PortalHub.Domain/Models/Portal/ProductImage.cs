using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Portal
{
    [Table("ProductImages", Schema = "Portal")]
    public class ProductImage
    {
        [Key]
        public long ProductImageId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = null!;

        public bool IsPrimary { get; set; } = false;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}