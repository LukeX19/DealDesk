using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Exceptions;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.DataAccess.Repositories
{
    public abstract class BaseRepositoryInMemory<T> : IBaseRepository<T> where T : BaseEntity
    {
        private static ICollection<T> _entities = new List<T>();
        private static long _nextId = 1;

        public async Task<long> Create(T entity, CancellationToken ct = default)
        {
            entity.Id = _nextId++;
            _entities.Add(entity);
            return await Task.FromResult(entity.Id);
        }

        public async Task<ICollection<T>> GetAll(CancellationToken ct = default)
        {
            return await Task.FromResult(_entities.ToList());
        }

        public async Task<T> GetById(long id, CancellationToken ct = default)
        {
            var entity = _entities.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            return await Task.FromResult(entity);
        }

        public async Task Update(long id, T updatedEntity, CancellationToken ct = default)
        {
            var existingEntity = _entities.FirstOrDefault(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            updatedEntity.Id = id;
            _entities.Remove(existingEntity);
            _entities.Add(updatedEntity);
            await Task.CompletedTask;
        }

        public async Task Delete(long id, CancellationToken ct = default)
        {
            var entity = _entities.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            _entities.Remove(entity);
            await Task.CompletedTask;
        }
    }
}
