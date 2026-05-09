using AutoMapper;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.EnquiryMst;
using PortalHub.Application.Interfaces.Portal;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.EnquiryMst;
using PortalHub.Domain.Models.Portal;

public class EnquiryService : IEnquiryService
{
    private readonly IRepository<Enquiry> _repo;
    private readonly IQueryRepository<Enquiry> _query;
    private readonly IRepository<EnquiryFollowup> _followRepo;
    private readonly IRepository<EnquiryForward> _forwardRepo;
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Product> _productQuery;
    private readonly IQueryRepository<User> _userQuery;

    private readonly IQueryRepository<EnquiryStatus> _statusQuery;

    public EnquiryService(
    IRepository<Enquiry> repo,
    IQueryRepository<Enquiry> query,
    IRepository<EnquiryFollowup> followRepo,
    IRepository<EnquiryForward> forwardRepo,
    IQueryRepository<EnquiryStatus> statusQuery,
    IQueryRepository<Product> productQuery,
    IQueryRepository<User> userQuery,
    IMapper mapper)
{
    _repo = repo;
    _query = query;
    _followRepo = followRepo;
    _forwardRepo = forwardRepo;
    _statusQuery = statusQuery;
    _productQuery = productQuery;
    _userQuery = userQuery;
    _mapper = mapper;
}

    public async Task<ServiceResult<long>> CreateAsync(CreateEnquiryDto dto)
    {
        var entity = _mapper.Map<Enquiry>(dto);

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        return ServiceResult<long>.Ok(entity.EnquiryId, "Enquiry created successfully");
    }

    

    public async Task<ServiceResult<bool>> UpdateStatusAsync(UpdateEnquiryDto dto)
    {
        var entity = await _query.GetByIdAsync(dto.EnquiryId);

        if (entity == null)
           return ServiceResult<bool>.Fail("Not found", ErrorCodes.NotFound);

        entity.StatusId = dto.StatusId;
        entity.AssignedToUserId = dto.AssignedToUserId;

        await _repo.UpdateAsync(entity);
        await _repo.SaveChangesAsync();

       return ServiceResult<bool>.Ok(true, "Enquiry status updated successfully");
    }

    public async Task<ServiceResult<bool>> AddFollowupAsync(AddEnquiryFollowupDto dto)
    {
        var follow = _mapper.Map<EnquiryFollowup>(dto);

        follow.CreatedAt = DateTime.UtcNow;
        follow.CreatedBy = 1; // TODO: replace with logged user

        await _followRepo.AddAsync(follow);
        await _followRepo.SaveChangesAsync();

       return ServiceResult<bool>.Ok(true, "Followup added successfully");
    }

    public async Task<ServiceResult<bool>> ForwardAsync(ForwardEnquiryDto dto)
    {
        var forward = _mapper.Map<EnquiryForward>(dto);

        forward.FromUserId = 1; // TODO: logged user

        await _forwardRepo.AddAsync(forward);
        await _forwardRepo.SaveChangesAsync();

        return ServiceResult<bool>.Ok(true, "Enquiry forwarded successfully");
    }

  public async Task<ServiceResult<EnquiryDto>> GetByIdAsync(long id)
{
    var entity = await _query.GetByIdAsync(id);

    if (entity == null)
    {
        return ServiceResult<EnquiryDto>
            .Fail("Not found", ErrorCodes.NotFound);
    }

    var dto = _mapper.Map<EnquiryDto>(entity);

    // STATUS
    dto.StatusName = "Unknown";

    if (entity.StatusId > 0)
    {
        var status = await _statusQuery.GetByIdAsync(entity.StatusId);
        dto.StatusName = status?.StatusName ?? "Unknown";
    }

    // PRODUCT
    if (entity.ProductId.HasValue)
    {
        var product = await _productQuery
            .GetByIdAsync(entity.ProductId.Value);

        dto.ProductName = product?.Name;
    }

    // ASSIGNED USER
    if (entity.AssignedToUserId.HasValue)
    {
        var user = await _userQuery
            .GetByIdAsync(entity.AssignedToUserId.Value);

        dto.AssignedToUserName = user?.FirstName +" "+user?.LastName;
    }

    return ServiceResult<EnquiryDto>.Ok(dto, "Retrieved successfully");
}

public async Task<ServiceResult<IEnumerable<EnquiryStatusDto>>> GetStatusesAsync()
{
    var statuses = await _statusQuery.GetAllAsync();

    var dto = _mapper.Map<IEnumerable<EnquiryStatusDto>>(statuses);

    return ServiceResult<IEnumerable<EnquiryStatusDto>>
        .Ok(dto, "Statuses retrieved successfully");
}
}