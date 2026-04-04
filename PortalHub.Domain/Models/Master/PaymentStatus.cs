using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Domain.Models.Master
{
    public class PaymentStatus
    {
        public int PaymentStatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
