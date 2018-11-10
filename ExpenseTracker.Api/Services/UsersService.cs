using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Api.Services
{
    public interface IUsersService
    {
        Task<UserDto> RegisterUserAsync(RegisterUserDto userRegistrationDto);
    }

    public class UsersService : IUsersService
    {
        private readonly ExpenseTrackerDbContext _dbContext;
        
        public UsersService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<UserDto> RegisterUserAsync(RegisterUserDto userRegistrationDto)
        {
            if (userRegistrationDto == null) throw new ArgumentNullException(nameof(userRegistrationDto));
            
            return Task.FromResult(new UserDto
            {
                Id = 1,
                Name = userRegistrationDto.Name,
                Surname = userRegistrationDto.Surname,
                Email = userRegistrationDto.Email
            });
        }

        public Task<UserDto> FindByEmail(string email)
        {
            return Task.FromResult(new UserDto
            {
                Id = 1,
                Name = string.Empty,
                Surname = string.Empty,
                Email = string.Empty
            });
        }
    }
}
