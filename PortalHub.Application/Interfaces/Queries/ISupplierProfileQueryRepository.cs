using PortalHub.Application.DTOs.Portal;

namespace PortalHub.Application.Interfaces.Queries
{
    public interface ISupplierProfileQueryRepository
    {
        Task<IEnumerable<SupplierProfileResponseDto>> GetAllAsync();

        Task<SupplierProfileResponseDto?> GetByIdAsync(long supplierId);
    }
}