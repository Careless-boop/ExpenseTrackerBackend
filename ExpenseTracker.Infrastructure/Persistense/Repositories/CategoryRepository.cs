using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Persistense.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ExpenseTrackerContext _dbContext;

        public CategoryRepository(ExpenseTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> GetByNameAndUserAsync(string name, string userId)
        {
            return await _dbContext.Categories
                .Where(c => c.Name == name && c.UserId == userId)
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetAllForUserAsync(string userId)
        {
            return await _dbContext.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
