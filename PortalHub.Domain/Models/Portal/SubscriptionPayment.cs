using PortalHub.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PortalHub.Domain.Models.Portal
{
    [Table("SubscriptionPayments", Schema = "Portal")]
    public class SubscriptionPayment
    {
        [Key]
        public long SubscriptionPaymentId { get; set; }
        public long SubscriptionOrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }
        public string? TransactionId { get; set; }
        public int PaymentStatusId { get; set; }
        public DateTime CreatedAt { get; set; }

        public SubscriptionOrder Order { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
