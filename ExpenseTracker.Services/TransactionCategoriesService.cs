using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Api.Data;
using ExpenseTracker.Data.Models;
using ExpenseTracker.DTO;
using ExpenseTracker.Services.Extensions;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Services
{
    public class TransactionCategoriesService : ITransactionCategoriesService
    {
        private readonly ExpenseTrackerDbContext _dbContext;
        private readonly ILogger<TransactionCategoriesService> _logger;

        public TransactionCategoriesService(ExpenseTrackerDbContext dbContext, ILogger<TransactionCategoriesService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionCategoryDto>> ListAllTransactionCategoriesAsync()
        {
            var transactionCategories = await _dbContext.TransactionCategories.ToListAsync();
            return transactionCategories.Select(c => c.ToDto()).ToList();
        }

        public async Task<TransactionCategoryDto> FindTransactionCategoryByIdAsync(int id)
        {
            var transactionCategory = await _dbContext.TransactionCategories.SingleElementAsync(c => c.TransactionCategoryId == id);
            return transactionCategory.ToDto();
        }

        public async Task<TransactionCategoryDto> CreateTransactionCategoryAsync(CreateTransactionCategoryDto createTransactionCategoryDto)
        {
            if (createTransactionCategoryDto == null) throw new ArgumentNullException(nameof(createTransactionCategoryDto));

            var entityToCreate = new TransactionCategory
            {
                Name = createTransactionCategoryDto.Name
            };
            _dbContext.TransactionCategories.Add(entityToCreate);
            await _dbContext.SaveChangesAsync();

            return entityToCreate.ToDto();
        }

        public async Task<TransactionCategoryDto> UpdateTransactionCategoryAsync(int id, TransactionCategoryDto transactionCategoryDto)
        {
            if (transactionCategoryDto == null) throw new ArgumentNullException(nameof(transactionCategoryDto));

            var transactionCategory = await _dbContext.TransactionCategories.SingleElementAsync(c => c.TransactionCategoryId == id);
            transactionCategory.Name = transactionCategoryDto.Name;
            await _dbContext.SaveChangesAsync();

            return transactionCategory.ToDto();
        }

        public async Task DeleteTransactionCategoryAsync(int id)
        {
            var transactionCategory = await _dbContext.TransactionCategories.SingleElementAsync(c => c.TransactionCategoryId == id);
            _dbContext.TransactionCategories.Remove(transactionCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}