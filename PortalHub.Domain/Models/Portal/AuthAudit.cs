using System;
using System.Collections.Generic;
using System.Text;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Domain.Models.Portal
{
    public class AuthAuditLog
    {
        public long AuditLogId { get; set; }

        public long? UserId { get; set; }

        public string Email { get; set; }

        public bool IsSuccess { get; set; }

        public string EventType { get; set; }

        public string? Reason { get; set; }

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }

        public DateTime CreatedAt { get; set; }
        public virtual User? User { get; set; }
    }

public class BlockedIp
{
    public long Id { get; set; }

    public string IpAddress { get; set; }

    public DateTime BlockedAt { get; set; }

    public DateTime BlockedUntil { get; set; }

    public string Reason { get; set; }
}
}
