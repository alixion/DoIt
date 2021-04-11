using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using DoIt.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace DoIt.Infrastructure.Data.EfRepositories
{
    public class EfRepository<TEntity, TKey>:IRepository<TEntity, TKey> where TEntity: Entity, IAggregateRoot
    {
        private readonly DoItDbContext _context;

        public EfRepository(DoItDbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity item)
        {
            await _context.Set<TEntity>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync<TEntity>(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity item)
        {
            _context.Set<TEntity>().Remove(item);
            await _context.SaveChangesAsync();
        }
        
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }
    }
}