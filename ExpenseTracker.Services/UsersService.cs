using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Api.Data;
using ExpenseTracker.Data.Models;
using ExpenseTracker.DTO;
using ExpenseTracker.Services.Exceptions;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class UsersService : IUsersService
    {
        private readonly ExpenseTrackerDbContext _dbContext;
        private readonly IPasswordHasher<UserDto> _passwordHasher;

        public UsersService(ExpenseTrackerDbContext dbContext, IPasswordHasher<UserDto> passwordHasher)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<UserDto> RegisterUserAsync(RegisterUserDto userRegistrationDto)
        {
            if (userRegistrationDto == null) throw new ArgumentNullException(nameof(userRegistrationDto));

            var emailAlreadyRegistered = await _dbContext.Users.Where(u => u.Email == userRegistrationDto.Email).AnyAsync();
            if (emailAlreadyRegistered)
                throw new ConflictException("A user is already registered with the supplied email address");

            var entityToCreate = new User
            {
                UserGuid = Guid.NewGuid(),
                Name = userRegistrationDto.Name,
                Surname = userRegistrationDto.Surname,
                Email = userRegistrationDto.Email,
                HashedPassword = _passwordHasher.HashPassword(null, userRegistrationDto.Password),
                IsActive = true
            };
            _dbContext.Users.Add(entityToCreate);
            await _dbContext.SaveChangesAsync();
            return entityToCreate.ToDto();
        }

        public async Task<UserDto> FindUserByUserGuidAsync(Guid userGuid)
        {
            var user = await _dbContext.Users.SingleElementAsync(u => u.UserGuid == userGuid);
            return user.ToDto();
        }

        public async Task<UserDto> FindUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users.SingleElementAsync(u => u.Email == email);
            return user.ToDto();
        }

        public async Task<bool> VerifyUserPassword(UserDto userDto, string password)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var user = await _dbContext.Users.SingleElementAsync(u => u.UserGuid == userDto.Id);

            var verificationResult = _passwordHasher.VerifyHashedPassword(null, user.HashedPassword, password);
            return verificationResult == PasswordVerificationResult.Success;
        }

        public async Task<UserDto> UpdateUserAsync(Guid userGuid, UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));

            var user = await _dbContext.Users.SingleElementAsync(u => u.UserGuid == userGuid);
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            await _dbContext.SaveChangesAsync();

            return user.ToDto();
        }        
    }
}
