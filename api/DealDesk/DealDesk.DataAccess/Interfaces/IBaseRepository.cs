using DealDesk.DataAccess.Entities;

namespace DealDesk.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<long> Create(T entity, CancellationToken ct = default);
        Task<ICollection<T>> GetAll(CancellationToken ct = default);
        Task<T> GetById(long id, CancellationToken ct = default);
        Task Update(long id, T updatedEntity, CancellationToken ct = default);
        Task Delete(long id, CancellationToken ct = default);
    }
}
