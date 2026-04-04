using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Portal
{
    public class CreateSupplierProfileDto
    {
        public long SupplierId { get; set; }   // = UserId
        public string? GSTNumber { get; set; }
        public string BusinessName { get; set; } = null!;
        public string OfficeAddressLine1 { get; set; } = null!;
        public string? OfficeAddressLine2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Pincode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
    public class UpdateSupplierProfileDto
    {
        public long SupplierId { get; set; }
        public string? GSTNumber { get; set; }
        public string BusinessName { get; set; } = null!;
        public string OfficeAddressLine1 { get; set; } = null!;
        public string? OfficeAddressLine2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Pincode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
    public class SupplierProfileResponseDto
    {
        public long SupplierId { get; set; }
        public string? GSTNumber { get; set; }
        public string BusinessName { get; set; } = null!;
        public string OfficeAddressLine1 { get; set; } = null!;
        public string? OfficeAddressLine2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Pincode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public bool IsGSTVerified { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
