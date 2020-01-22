using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Domain;
using DAL.Interfaces;

namespace IntegrationTest.Stubs
{
    internal class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseDomain
    {
        private readonly List<TEntity> _context;

        public GenericRepository()
        {
            _context = new List<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return (new List<TEntity>(_context)).AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Task.FromResult(_context[id - 1]);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _context.Add(entity);
            entity.Id = _context.Count;
            return await Task.FromResult(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context[entity.Id - 1] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _context.RemoveAt(id - 1);
            return Task.CompletedTask;
        }
    }
}
