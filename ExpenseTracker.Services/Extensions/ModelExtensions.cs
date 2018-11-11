using ExpenseTracker.Api.Dtos;
using ExpenseTracker.Data.Models;

namespace ExpenseTracker.Services
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