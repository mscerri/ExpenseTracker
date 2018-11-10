using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Entities
{
    public class Transaction
    {
        public long TransactionId { get; set; }

        [Required]
        public User User { get; set; }

        public long UserId { get; set; }

        [Required]
        public TransactionCategory TransactionCategory { get; set; }

        public int TransactionCategoryId { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
