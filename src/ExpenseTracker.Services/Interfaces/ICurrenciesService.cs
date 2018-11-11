using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.DTO;

namespace ExpenseTracker.Services.Interfaces
{
    public interface ICurrenciesService
    {
        Task<IEnumerable<CurrencyDto>> ListAllCurrenciesAsync();
        Task<CurrencyDto> FindCurrencyByIdAsync(int id);
        Task<CurrencyDto> CreateCurrencyAsync(CreateCurrencyDto createCurrencyDto);
        Task<CurrencyDto> UpdateCurrencyAsync(int id, CurrencyDto currencyDto);
        Task DeleteCurrencyAsync(int id);
    }
}