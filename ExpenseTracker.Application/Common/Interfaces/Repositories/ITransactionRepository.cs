using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Transaction>> GetAllForUserAsync(string userId);
    }
}
