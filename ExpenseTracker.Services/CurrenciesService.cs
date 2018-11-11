using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Dtos;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public CurrenciesService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<CurrencyDto>> ListAllCurrenciesAsync()
        {
            var currencies = await _dbContext.Currencies.ToListAsync();
            return currencies.Select(c => c.ToDto()).ToList();
        }

        public async Task<CurrencyDto> FindCurrencyByIdAsync(int id)
        {
            var currency = await _dbContext.Currencies.SingleElementAsync(c => c.CurrencyId == id);
            return currency.ToDto();
        }

        public async Task<CurrencyDto> CreateCurrencyAsync(CreateCurrencyDto createCurrencyDto)
        {
            if (createCurrencyDto == null) throw new ArgumentNullException(nameof(createCurrencyDto));

            var entityToCreate = new Currency
            {
                Name = createCurrencyDto.Name
            };
            _dbContext.Currencies.Add(entityToCreate);
            await _dbContext.SaveChangesAsync();

            return entityToCreate.ToDto();
        }

        public async Task<CurrencyDto> UpdateCurrencyAsync(int id, CurrencyDto currencyDto)
        {
            if (currencyDto == null) throw new ArgumentNullException(nameof(currencyDto));

            var currency = await _dbContext.Currencies.SingleElementAsync(c => c.CurrencyId == id);
            currency.Name = currencyDto.Name;
            await _dbContext.SaveChangesAsync();

            return currency.ToDto();
        }

        public async Task DeleteCurrencyAsync(int id)
        {
            var currency = await _dbContext.Currencies.SingleElementAsync(c => c.CurrencyId == id);
            _dbContext.Currencies.Remove(currency);
            await _dbContext.SaveChangesAsync();
        }
    }
}