using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Exceptions;
using DealDesk.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DealDesk.DataAccess.Repositories
{
    public abstract class BaseRepositorySQLite<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        protected BaseRepositorySQLite(AppDbContext context)
        {
            _context = context;
        }

        public long Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public ICollection<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(long id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity ?? throw new EntityNotFoundException(typeof(T).Name, id);
        }

        public void Update(long id, T updatedEntity)
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            _context.Set<T>().Remove(existingEntity);
            updatedEntity.Id = id;
            _context.Add(updatedEntity);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
