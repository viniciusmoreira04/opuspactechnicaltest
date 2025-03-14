using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Interfaces
{
    public interface IPostgreConnection<T> where T : BaseEntity
    {
        Task<T?> GetAsync(long id);
        Task InsertAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task RemoveAsync(long id);
        Task<User> GetUserAsync(string email);
        Task<Product> GetProductAsync(string description);
    }
}
