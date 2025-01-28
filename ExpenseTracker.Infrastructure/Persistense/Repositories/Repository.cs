using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Persistense.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        private readonly ExpenseTrackerContext _dbContext = null!;

        protected Repository(ExpenseTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            var entity = _dbContext.Set<T>().FirstOrDefault(i => i.Id == id);
            if(entity == null)
            {
                return Task.FromResult(false);
            }

            _dbContext.Set<T>().Remove(entity);
            
            return Task.FromResult(true);
        }
    }
}
