using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }

        [Required]
        public Guid TransactionGuid { get; set; }

        [Required]
        public User User { get; set; }

        public long UserId { get; set; }

        [Required]
        public DateTimeOffset ExecutionDate { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public TransactionCategory TransactionCategory { get; set; }

        public int TransactionCategoryId { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdateAt { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
