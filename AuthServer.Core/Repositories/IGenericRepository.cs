using System.Linq.Expressions;

namespace AuthServer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();//herhangi bir sorgu olmayacağı için IEnumerable kullandık
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);//sorgu olacağı için IQueryable kullandık
        Task AddAsync(T entity);
        void Remove(T entity);
        T Update(T entity);
    }
}
