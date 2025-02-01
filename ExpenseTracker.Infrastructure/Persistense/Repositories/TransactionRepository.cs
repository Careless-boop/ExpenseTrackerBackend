using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Persistense.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly ExpenseTrackerContext _dbContext = null!;

        public TransactionRepository(ExpenseTrackerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> GetAllByCategoryAsync(Guid categoryId)
        {
            return await _dbContext.Transactions.Where(t => t.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllForUserAsync(string userId)
        {
            return await _dbContext.Transactions
                .Include(t => t.Category)                  
                .Where(t => t.Category!.UserId == userId)
                .ToListAsync();
        }
    }
}
