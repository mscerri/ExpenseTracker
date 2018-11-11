using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Data.Models
{
    public class TransactionCategory
    {
        public int TransactionCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
