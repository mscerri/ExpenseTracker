using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Dtos;
using ExpenseTracker.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Services
{
    public class TransactionCategoriesService : ITransactionCategoriesService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public TransactionCategoriesService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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