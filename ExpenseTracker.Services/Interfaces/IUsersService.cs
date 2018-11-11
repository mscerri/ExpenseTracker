using System;
using System.Threading.Tasks;
using ExpenseTracker.DTO;

namespace ExpenseTracker.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDto> RegisterUserAsync(RegisterUserDto userRegistrationDto);
        Task<UserDto> FindUserByUserGuidAsync(Guid userGuid);
        Task<UserDto> FindUserByEmailAsync(string email);
        Task<bool> VerifyUserPassword(UserDto userDto, string password);
        Task<UserDto> UpdateUserAsync(Guid userGuid, UserDto userDto);
    }
}