using DealDesk.DataAccess.Entities;

namespace DealDesk.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        long Create(T entity);
        ICollection<T> GetAll();
        T GetById(long id);
        void Update(long id, T updatedEntity);
        void Delete(long id);
    }
}
