using PortalHub.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PortalHub.Domain.Models.Portal
{
    [Table("UserSubscriptions", Schema = "Portal")]
    public class UserSubscription
    {
        [Key]
        public long SubscriptionId { get; set; }
        public long UserId { get; set; }
        public int PlanId { get; set; }
        public long SubscriptionOrderId { get; set; }
        public int PaymentStatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public SubscriptionOrder Order { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
