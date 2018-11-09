using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Entities
{
    public class User
    {
        public long UserId { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
