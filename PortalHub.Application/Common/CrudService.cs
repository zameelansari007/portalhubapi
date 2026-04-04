using AutoMapper;
using FluentValidation;
using PortalHub.Application.Common;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;

public class CrudService<TEntity, TCreateDto, TUpdateDto, TResponseDto> :
    ICrudService<TCreateDto, TUpdateDto, TResponseDto>
    where TEntity : class
{
    protected readonly IQueryRepository<TEntity> _queryRepo;
    protected readonly IRepository<TEntity> _commandRepo;
    protected readonly IValidator<TEntity>? _validator;
    protected readonly IMapper _mapper;

    public CrudService(
        IQueryRepository<TEntity> queryRepo,
        IRepository<TEntity> commandRepo,
        IMapper mapper,
        IValidator<TEntity>? validator = null)
    {
        _queryRepo = queryRepo;
        _commandRepo = commandRepo;
        _mapper = mapper;
        _validator = validator;
    }

    public virtual async Task<IEnumerable<TResponseDto>> GetAllAsync()
    {
        var entities = await _queryRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<TResponseDto>>(entities);
    }

    public virtual async Task<TResponseDto?> GetByIdAsync(object id)
    {
        var entity = await _queryRepo.GetByIdAsync(id);
        return entity == null ? default : _mapper.Map<TResponseDto>(entity);
    }

    public virtual async Task<ServiceResult<TResponseDto>> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        if (_validator != null)
        {
            var result = await _validator.ValidateAsync(entity);
            if (!result.IsValid)
                return ServiceResult<TResponseDto>.Fail(
                    result.Errors.First().ErrorMessage,
                    ErrorCodes.ValidationError);
        }

        await _commandRepo.AddAsync(entity);
        await _commandRepo.SaveChangesAsync();

        return ServiceResult<TResponseDto>.Ok(
            _mapper.Map<TResponseDto>(entity));
    }

    public virtual async Task<ServiceResult<TResponseDto>> UpdateAsync(TUpdateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        if (_validator != null)
        {
            var result = await _validator.ValidateAsync(entity);
            if (!result.IsValid)
                return ServiceResult<TResponseDto>.Fail(
                    result.Errors.First().ErrorMessage,
                    ErrorCodes.ValidationError);
        }

        await _commandRepo.UpdateAsync(entity);
        await _commandRepo.SaveChangesAsync();

        return ServiceResult<TResponseDto>.Ok(
            _mapper.Map<TResponseDto>(entity));
    }

    public virtual async Task<ServiceResult<bool>> DeleteAsync(object id)
    {
        var entity = await _queryRepo.GetByIdAsync(id);
        if (entity == null)
            return ServiceResult<bool>.Fail(
                "Not found",
                ErrorCodes.NotFound);

        await _commandRepo.DeleteAsync(entity);
        await _commandRepo.SaveChangesAsync();

        return ServiceResult<bool>.Ok(true);
    }
}
