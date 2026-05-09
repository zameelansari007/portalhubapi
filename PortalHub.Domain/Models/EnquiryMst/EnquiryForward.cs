using PortalHub.Domain.Models.Portal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.EnquiryMst
{
    [Table("EnquiryForwards", Schema = "Portal")]
public class EnquiryForward
{
    [Key]
    public long ForwardId { get; set; }

    public long EnquiryId { get; set; }
    public long FromUserId { get; set; }
    public long ToUserId { get; set; }

    public string? Remarks { get; set; }
    public DateTime ForwardedAt { get; set; }
}
    
}