using PortalHub.Application.Common;

public interface ICrudService<TCreateDto, TUpdateDto, TResponseDto>
{
    /* READ */
    Task<IEnumerable<TResponseDto>> GetAllAsync();
    Task<TResponseDto?> GetByIdAsync(object id);

    /* WRITE */
    Task<ServiceResult<TResponseDto>> CreateAsync(TCreateDto dto);
    Task<ServiceResult<TResponseDto>> UpdateAsync(TUpdateDto dto);
    Task<ServiceResult<bool>> DeleteAsync(object id);
}
