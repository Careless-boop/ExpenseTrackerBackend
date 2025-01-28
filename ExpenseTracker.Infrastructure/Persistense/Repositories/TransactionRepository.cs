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

        public async Task<IEnumerable<Transaction>> GetAllForUserAsync(string userId)
        {
            return await _dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}
