using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Exceptions;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.DataAccess.Repositories
{
    public abstract class BaseRepositoryInMemory<T> : IBaseRepository<T> where T : BaseEntity
    {
        private static ICollection<T> _entities = new List<T>();
        private static long _nextId = 1;

        public long Create(T entity)
        {
            entity.Id = _nextId++;
            _entities.Add(entity);
            return entity.Id;
        }

        public ICollection<T> GetAll()
        {
            return _entities;
        }

        public T GetById(long id)
        {
            return _entities.FirstOrDefault(x => x.Id == id) ?? throw new EntityNotFoundException(typeof(T).Name, id);
        }

        public void Update(long id, T updatedEntity)
        {
            var existingEntity = _entities.FirstOrDefault(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            updatedEntity.Id = id;
            _entities.Remove(existingEntity);
            _entities.Add(updatedEntity);
        }

        public void Delete(long id)
        {
            var entity = _entities.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            _entities.Remove(entity);
        }
    }
}
