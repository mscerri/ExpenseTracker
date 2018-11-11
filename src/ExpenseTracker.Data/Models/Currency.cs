using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Data.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdateAt { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
