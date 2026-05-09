using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Master
{
    [Table("Countries", Schema = "Master")]
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CountryName { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string CountryCode { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public ICollection<State> States { get; set; }
            = new List<State>();
    }
}