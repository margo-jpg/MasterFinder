using MasterFinder.Domain.Base;

namespace MasterFinder.Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}