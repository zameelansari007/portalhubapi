using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Master
{
    [Table("Cities", Schema = "Master")]
    public class City
    {
        [Key]
        public int CityId { get; set; }

        public int StateId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CityName { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(StateId))]
        public State State { get; set; } = null!;
    }
}