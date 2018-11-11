using ExpenseTracker.Data.Models;
using ExpenseTracker.DTO;

namespace ExpenseTracker.Services.Extensions
{
    public static class ModelExtensions
    {
        public static UserDto ToDto(this User model)
        {
            return new UserDto
            {
                Id = model.UserGuid,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                IsActive = model.IsActive
            };
        }

        public static TransactionDto ToDto(this Transaction transaction)
        {
            return new TransactionDto()
            {
                Id = transaction.TransactionGuid,
                Amount = transaction.Amount,
                Currency = transaction.Currency.ToDto(),
                Category = transaction.TransactionCategory.ToDto(),
                Note = transaction.Note,
                ExecutionDate = transaction.ExecutionDate
            };
        }

        public static TransactionCategoryDto ToDto(this TransactionCategory model)
        {
            return new TransactionCategoryDto
            {
                Id = model.TransactionCategoryId,
                Name = model.Name
            };
        }

        public static CurrencyDto ToDto(this Currency model)
        {
            return new CurrencyDto
            {
                Id = model.CurrencyId,
                Name = model.Name
            };
        }
    }
}