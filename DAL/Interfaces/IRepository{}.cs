using System.Linq;
using System.Threading.Tasks;
using DAL.Domain;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : BaseDomain
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);
    }
}
