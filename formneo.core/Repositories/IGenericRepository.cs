using System.Linq.Expressions;

namespace vesa.core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdStringAsync(string id);

        Task<T> GetByIdStringGuidAsync(Guid id);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

    }
}
