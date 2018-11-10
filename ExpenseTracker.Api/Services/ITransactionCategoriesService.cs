﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Api.Dtos;

namespace ExpenseTracker.Api.Services
{
    public interface ITransactionCategoriesService
    {
        Task<IEnumerable<TransactionCategoryDto>> ListAllTransactionCategoriesAsync();
        Task<TransactionCategoryDto> FindTransactionCategoryByIdAsync(int id);
        Task<TransactionCategoryDto> CreateTransactionCategoryAsync(CreateTransactionCategoryDto createTransactionCategoryDto);
        Task<TransactionCategoryDto> UpdateTransactionCategoryAsync(int id, TransactionCategoryDto transactionCategoryDto);
        Task DeleteTransactionCategoryAsync(int id);
    }
}