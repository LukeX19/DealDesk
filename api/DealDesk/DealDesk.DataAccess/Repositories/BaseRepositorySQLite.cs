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

        public async Task<long> Create(T entity, CancellationToken ct = default)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<ICollection<T>> GetAll(CancellationToken ct = default)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(ct);
        }

        public async Task<T> GetById(long id, CancellationToken ct = default)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, ct);
            return entity ?? throw new EntityNotFoundException(typeof(T).Name, id);
        }

        public async Task Update(long id, T updatedEntity, CancellationToken ct = default)
        {
            var existingEntity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, ct);
            if (existingEntity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            _context.Set<T>().Remove(existingEntity);
            updatedEntity.Id = id;
            _context.Add(updatedEntity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(long id, CancellationToken ct = default)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, ct);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
