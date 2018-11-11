using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTO
{
    public class CreateTransactionDto
    {
        [Required]
        public decimal? Amount { get; set; }

        [Required]
        public int? CurrencyId { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        public string Note { get; set; }

        [Required]
        public DateTimeOffset? ExecutionDate { get; set; }
    }
}
