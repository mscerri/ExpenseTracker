using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
