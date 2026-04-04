using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Portal
{
    [Table("Categories", Schema = "Portal")]
    public class Category
    {
        [Key]
        public long CategoryId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string Slug { get; set; } = null!;

        public long? ParentId { get; set; }

        [Required]
        [MaxLength(500)]
        public string IdPath { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string SlugPath { get; set; } = null!;

        public int Level { get; set; } = 0;

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        // 🔹 Self-reference
        [ForeignKey(nameof(ParentId))]
        public Category? Parent { get; set; }

        public ICollection<Category> Children { get; set; } = new List<Category>();

        // 🔹 Navigation
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
