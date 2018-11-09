using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Api.Dtos;

namespace ExpenseTracker.Api.Services
{
    public interface IUsersService
    {
        Task<UserDto> RegisterUserAsync(RegisterUserDto userRegistrationDto);
    }

    public class UsersService : IUsersService
    {
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
    }
}
