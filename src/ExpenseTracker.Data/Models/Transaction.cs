using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Data.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }

        [Required]
        public Guid TransactionGuid { get; set; }

        [Required]
        public User User { get; set; }

        public long UserId { get; set; }

        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Required]
        public Currency Currency { get; set; }

        public int CurrencyId { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        [Required]
        public TransactionCategory TransactionCategory { get; set; }

        public int TransactionCategoryId { get; set; }

        [Required]
        public DateTimeOffset ExecutionDate { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdateAt { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
