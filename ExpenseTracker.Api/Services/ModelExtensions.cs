using ExpenseTracker.Api.Dtos;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Services
{
    public static class ModelExtensions
    {
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