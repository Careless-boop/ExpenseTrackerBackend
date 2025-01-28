using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Infrastructure.Persistense.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ExpenseTrackerContext _dbContext;
        private ITransactionRepository? _transactionRepository = null;
        private ICategoryRepository? _categoryRepository = null;

        public RepositoryWrapper(ExpenseTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(_dbContext);
                }
                return _transactionRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_dbContext);
                }
                return _categoryRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
