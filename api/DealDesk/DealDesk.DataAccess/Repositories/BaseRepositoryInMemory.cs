using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.DataAccess.Repositories
{
    public abstract class BaseRepositoryInMemory<T> : IBaseRepository<T> where T : BaseEntity
    {
        private static ICollection<T> _entities = new List<T>();

        public void Create(T entity)
        {
            if (! _entities.Contains(entity))
            {
                _entities.Add(entity);
            }
        }

        public ICollection<T> GetAll()
        {
            return _entities;
        }

        public T? GetById(long id)
        {
            return _entities.FirstOrDefault(x => x.Id == id);
        }

        public void Update(long id, T updatedEntity)
        {
            var existingEntity = _entities.FirstOrDefault(x => x.Id == id);
            if (existingEntity != null)
            {
                _entities.Remove(existingEntity);
                _entities.Add(updatedEntity);
            }
        }

        public void Delete(long id)
        {
            var entity = _entities.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }
    }
}
