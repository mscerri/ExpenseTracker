using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Entities
{
    public class User
    {
        public long UserId { get; set; }

        [Required]
        public Guid UserSubjectId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public byte[] HashedPassword { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
