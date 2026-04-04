using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.Portal
{
    [Table("SupplierProfiles", Schema = "Portal")]
    public class SupplierProfile
    {
        [Key]
        public long SupplierId { get; set; }
        public string? GSTNumber { get; set; }
        public string BusinessName { get; set; }
        public string OfficeAddressLine1 { get; set; }
        public string? OfficeAddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public bool IsGSTVerified { get; set; }
        public DateTime? GSTVerifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; }
    }
}
