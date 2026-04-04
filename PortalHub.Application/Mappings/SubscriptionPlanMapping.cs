using AutoMapper;
using PortalHub.Application.DTOs.Master;
using PortalHub.Domain.Models;
using PortalHub.Domain.Models.Master;

public class SubscriptionPlanMapping : Profile
{
    public SubscriptionPlanMapping()
    {
        CreateMap<CreateSubscriptionPlanDto, SubscriptionPlan>();

        CreateMap<UpdateSubscriptionPlanDto, SubscriptionPlan>();

        CreateMap<SubscriptionPlan, SubscriptionPlanDto>();
    }
}
