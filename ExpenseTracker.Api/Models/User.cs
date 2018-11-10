using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Models
{
    public class User
    {
        public long UserId { get; set; }

        [Required]
        public Guid UserGuid { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
