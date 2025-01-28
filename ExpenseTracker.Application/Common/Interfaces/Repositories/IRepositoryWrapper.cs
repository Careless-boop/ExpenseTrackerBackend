namespace ExpenseTracker.Application.Common.Interfaces.Repositories
{
    public interface IRepositoryWrapper
    {
        ITransactionRepository TransactionRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        Task<int> SaveAsync();
    }
}
