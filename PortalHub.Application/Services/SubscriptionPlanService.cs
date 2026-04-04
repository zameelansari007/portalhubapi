using FluentValidation;
using AutoMapper;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Master;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Master;

public class SubscriptionPlanService :
    CrudService<
        SubscriptionPlan,
        CreateSubscriptionPlanDto,
        UpdateSubscriptionPlanDto,
        SubscriptionPlanDto>
{
    public SubscriptionPlanService(
        IQueryRepository<SubscriptionPlan> queryRepo,
        IRepository<SubscriptionPlan> commandRepo,
        IMapper mapper,
        IValidator<SubscriptionPlan> validator)
        : base(queryRepo, commandRepo, mapper, validator)
    {
    }

    // ==============================
    // BUSINESS RULE: Cannot delete active plan
    // ==============================
    public override async Task<ServiceResult<bool>> DeleteAsync(object id)
    {
        var entity = await _queryRepo.GetByIdAsync(id);

        if (entity == null)
            return ServiceResult<bool>.Fail("Not found", ErrorCodes.NotFound);

        if (entity.IsActive)
            return ServiceResult<bool>.Fail(
                "Active plans cannot be deleted",
                "PLAN_ACTIVE");

        return await base.DeleteAsync(id);
    }

    // ==============================
    // BUSINESS RULE: Price must be > 0
    // ==============================
    public override async Task<ServiceResult<SubscriptionPlanDto>> CreateAsync(
        CreateSubscriptionPlanDto dto)
    {
        if (dto.Price <= 0)
            return ServiceResult<SubscriptionPlanDto>.Fail(
                "Price must be greater than zero",
                "INVALID_PRICE");

        return await base.CreateAsync(dto);
    }
}
