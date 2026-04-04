namespace PortalHub.Application.DTOs.Master
{
    // READ
    public class SubscriptionPlanDto
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal Price { get; set; }
        public int MaxProducts { get; set; }
        public int DurationMonths { get; set; }
        public bool IsActive { get; set; }
    }

    // CREATE
    public class CreateSubscriptionPlanDto
    {
        public string PlanName { get; set; }
        public decimal Price { get; set; }
        public int MaxProducts { get; set; }
        public int DurationMonths { get; set; }
    }

    // UPDATE
    public class UpdateSubscriptionPlanDto
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal Price { get; set; }
        public int MaxProducts { get; set; }
        public int DurationMonths { get; set; }
        public bool IsActive { get; set; }
    }

    
}
