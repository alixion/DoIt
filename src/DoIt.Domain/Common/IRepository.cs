using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace DoIt.Domain.Common
{
    public interface IRepository<TEntity, in TKey> where TEntity: Entity, IAggregateRoot
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> ListAsync();
        Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec);
        
        Task<TEntity> AddAsync(TEntity item);
        Task UpdateAsync<TEntity>(TEntity item);
        Task DeleteAsync(TEntity item);
    }
}