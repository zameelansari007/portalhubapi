using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Master
{
    [Table("States", Schema = "Master")]
    public class State
    {
        [Key]
        public int StateId { get; set; }

        public int CountryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StateName { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; } = null!;

        public ICollection<City> Cities { get; set; }
            = new List<City>();
    }
}