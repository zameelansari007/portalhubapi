using PortalHub.Domain.Models.Portal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.EnquiryMst
{
[Table("EnquiryStatus", Schema = "Portal")]
public class EnquiryStatus
{
    [Key]
    public int EnquiryStatusId { get; set; }

    public string StatusName { get; set; }
}

}