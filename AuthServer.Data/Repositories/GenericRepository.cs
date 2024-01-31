using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthServer.Data.Repositories
{
    public class GenericRepository<T> : Core.Repositories.IGenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State |= EntityState.Modified;
            return entity;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
