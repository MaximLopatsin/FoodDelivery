using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.Domain;
using DAL.Interfaces;
using DAL.Orm;

namespace DAL.Repositories
{
    internal class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseDomain
    {
        private readonly DeliveryContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DeliveryContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var newEntity = _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return newEntity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var local = _context.Set<TEntity>()
                   .Local
                   .FirstOrDefault(entry => entry.Id == entity.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
