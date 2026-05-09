using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortalHub.Domain.Models.Master;

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

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public string Pincode { get; set; }

    public bool IsGSTVerified { get; set; }

    public DateTime? GSTVerifiedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; }

    public Country Country { get; set; }

    public State State { get; set; }

    public City City { get; set; }
}
}
