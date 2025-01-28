using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Infrastructure.Persistense.Repositories
{
    public class CategoryRepository(ExpenseTrackerContext dbContext) : Repository<Category>(dbContext), ICategoryRepository
    {
    }
}
