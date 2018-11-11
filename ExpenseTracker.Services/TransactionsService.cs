using ExpenseTracker.Api.Data;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public TransactionsService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
