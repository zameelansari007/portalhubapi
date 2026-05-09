using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.EnquiryMst
{

    public class EnquiryDto
{
    public long EnquiryId { get; set; }

    public long? ProductId { get; set; }
    public long? CustomerUserId { get; set; }

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Message { get; set; } = null!;

    public int StatusId { get; set; }
    public string StatusName { get; set; } = null!;

    public long? AssignedToUserId { get; set; }
     public string? AssignedToUserName { get; set; }

    public DateTime CreatedAt { get; set; }

    // optional (if you want UI display)
    public string? ProductName { get; set; }
}
public class CreateEnquiryDto
{
    public long? ProductId { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
}

public class UpdateEnquiryDto
{
    public long EnquiryId { get; set; }
    public int StatusId { get; set; }
    public long? AssignedToUserId { get; set; }
}

public class ForwardEnquiryDto
{
    public long EnquiryId { get; set; }
    public long ToUserId { get; set; }
    public string? Remarks { get; set; }
}

public class AddEnquiryFollowupDto
{
    public long EnquiryId { get; set; }

    public string Message { get; set; } = null!;
}

public class EnquiryFollowupDto
{
    public long FollowupId { get; set; }

    public long EnquiryId { get; set; }

    public string Message { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class EnquiryStatusDto
    {
        public int EnquiryStatusId { get; set; }

        public string StatusName { get; set; } = null!;
    }

}
