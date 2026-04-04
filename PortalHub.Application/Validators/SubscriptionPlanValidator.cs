using PortalHub.Domain.Models.Master;
using PortalHub.Application.Common;

namespace PortalHub.Application.Validators
{
    public class SubscriptionPlanValidator
        : BaseEntityValidator<SubscriptionPlan>
    {
        public SubscriptionPlanValidator()
        {
            RequireNotEmpty(x => x.PlanName, "Plan name");
            RequireNonNegative(x => x.Price, "Price");
            RequireMin(x => x.DurationMonths, 1, "Duration (months)");
            RequireMin(x => x.MaxProducts, 1, "Max products");
        }
    }
}
