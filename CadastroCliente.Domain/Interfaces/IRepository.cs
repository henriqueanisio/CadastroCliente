using CadastroCliente.Domain.Models;
using System.Linq.Expressions;

namespace CadastroCliente.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task DeleteAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
