
using PortalHub.Domain.Models.Portal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalHub.Domain.Models.EnquiryMst
{
[Table("Enquiries", Schema = "Portal")]
public class Enquiry
{
    [Key]
    public long EnquiryId { get; set; }

    public long? ProductId { get; set; }
    public long? CustomerUserId { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }

    public int StatusId { get; set; }
    public long? AssignedToUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Product? Product { get; set; }
}

}
