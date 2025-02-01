using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetByNameAndUserAsync(string name, string userId);
        Task<IEnumerable<Category>> GetAllForUserAsync(string userId);
    }
}
