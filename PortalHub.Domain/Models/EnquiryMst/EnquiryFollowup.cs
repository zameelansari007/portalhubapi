using PortalHub.Domain.Models.Portal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.EnquiryMst
{
    [Table("EnquiryFollowups", Schema = "Portal")]
public class EnquiryFollowup
{
    [Key]
    public long FollowupId { get; set; }

    public long EnquiryId { get; set; }
    public string Message { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
    
}