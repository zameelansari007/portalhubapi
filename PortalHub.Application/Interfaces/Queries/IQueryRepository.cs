namespace PortalHub.Application.Interfaces.Queries
{
    public interface IQueryRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
    }
}
