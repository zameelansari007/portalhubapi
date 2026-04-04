using PortalHub.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Domain.Models.Master
{
    [TableMap("SubscriptionPlans", "Master")]
    [KeyColumn("PlanId")]
    public class SubscriptionPlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal Price { get; set; }
        public int MaxProducts { get; set; }
        public int DurationMonths { get; set; }
        public bool IsActive { get; set; }
    }
}
