using PortalHub.Domain.Models.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PortalHub.Domain.Models.Portal
{
    [Table("SubscriptionOrders", Schema = "Portal")]
    public class SubscriptionOrder
    {
        [Key]
        public long SubscriptionOrderId { get; set; }
        public long UserId { get; set; }
        public int PlanId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentStatusId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
