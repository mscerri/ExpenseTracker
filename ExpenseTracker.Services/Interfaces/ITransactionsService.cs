using ExpenseTracker.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Interfaces
{
    public interface ITransactionsService
    {
        Task<IEnumerable<TransactionDto>> ListAllTransactionsForUserAsync(Guid userGuid);
        Task<TransactionDto> FindTransactionByGuidAsync(Guid transactionGuid);
        Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto, Guid userGuid);
        Task<TransactionDto> UpdateTransactionAsync(Guid transactionGuid, UpdateTransactionDto updateTransactionDto);
    }
}