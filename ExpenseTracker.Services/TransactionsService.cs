using ExpenseTracker.Api.Data;
using ExpenseTracker.Data.Models;
using ExpenseTracker.DTO;
using ExpenseTracker.Services.Extensions;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ExpenseTrackerDbContext _dbContext;
        private readonly ILogger<TransactionsService> _logger;

        public TransactionsService(ExpenseTrackerDbContext dbContext, ILogger<TransactionsService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionDto>> ListAllTransactionsForUserAsync(Guid userGuid)
        {
            var userTransactions = await _dbContext.Transactions
                .Include(t => t.Currency)
                .Include(t => t.TransactionCategory)
                .Where(t => t.User.UserGuid == userGuid)
                .OrderByDescending(t => t.ExecutionDate)
                .ToListAsync();

            return userTransactions.Select(t => t.ToDto()).ToList();
        }

        public async Task<TransactionDto> FindTransactionByGuidAsync(Guid transactionGuid)
        {
            var transaction = await _dbContext.Transactions
                .Include(t => t.Currency)
                .Include(t => t.TransactionCategory)
                .SingleElementAsync(t => t.TransactionGuid == transactionGuid);

            return transaction.ToDto();
        }

        public async Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto, Guid userGuid)
        {
            if (createTransactionDto == null) throw new ArgumentNullException(nameof(createTransactionDto));
            if (createTransactionDto.Amount == null) throw new ArgumentNullException(nameof(createTransactionDto.Amount));
            if (createTransactionDto.CurrencyId == null) throw new ArgumentNullException(nameof(createTransactionDto.CurrencyId));
            if (createTransactionDto.CategoryId == null) throw new ArgumentNullException(nameof(createTransactionDto.CategoryId));
            if (createTransactionDto.ExecutionDate == null) throw new ArgumentNullException(nameof(createTransactionDto.ExecutionDate));

            var user = await _dbContext.Users.SingleElementAsync(u => u.UserGuid == userGuid);
            var currency = await _dbContext.Currencies.SingleElementAsync(c => c.CurrencyId == createTransactionDto.CurrencyId);
            var transactionCategory = await _dbContext.TransactionCategories.SingleElementAsync(tc => tc.TransactionCategoryId == createTransactionDto.CategoryId);

            var entityToCreate = new Transaction
            {
                TransactionGuid = Guid.NewGuid(),
                UserId = user.UserId,
                Amount = createTransactionDto.Amount.Value,
                CurrencyId = currency.CurrencyId,
                Note = createTransactionDto.Note,
                TransactionCategoryId = transactionCategory.TransactionCategoryId,
                ExecutionDate = createTransactionDto.ExecutionDate.Value
            };
            _dbContext.Transactions.Add(entityToCreate);
            await _dbContext.SaveChangesAsync();

            return entityToCreate.ToDto();
        }

        public async Task<TransactionDto> UpdateTransactionAsync(Guid transactionGuid, UpdateTransactionDto updateTransactionDto)
        {
            if (updateTransactionDto == null) throw new ArgumentNullException(nameof(updateTransactionDto));
            if (updateTransactionDto.Amount == null) throw new ArgumentNullException(nameof(updateTransactionDto.Amount));
            if (updateTransactionDto.CurrencyId == null) throw new ArgumentNullException(nameof(updateTransactionDto.CurrencyId));
            if (updateTransactionDto.CategoryId == null) throw new ArgumentNullException(nameof(updateTransactionDto.CategoryId));
            if (updateTransactionDto.ExecutionDate == null) throw new ArgumentNullException(nameof(updateTransactionDto.ExecutionDate));

            var transaction = await _dbContext.Transactions.SingleElementAsync(t => t.TransactionGuid == transactionGuid);
            var currency = await _dbContext.Currencies.SingleElementAsync(c => c.CurrencyId == updateTransactionDto.CurrencyId);
            var transactionCategory = await _dbContext.TransactionCategories.SingleElementAsync(tc => tc.TransactionCategoryId == updateTransactionDto.CategoryId);

            transaction.Amount = updateTransactionDto.Amount.Value;
            transaction.CurrencyId = currency.CurrencyId;
            transaction.Note = updateTransactionDto.Note;
            transaction.TransactionCategoryId = transactionCategory.TransactionCategoryId;
            transaction.ExecutionDate = updateTransactionDto.ExecutionDate.Value;
            await _dbContext.SaveChangesAsync();

            return transaction.ToDto();
        }
    }
}
