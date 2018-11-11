using System;

namespace ExpenseTracker.DTO
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public CurrencyDto Currency { get; set; }

        public TransactionCategoryDto Category { get; set; }

        public string Note { get; set; }

        public DateTimeOffset ExecutionDate { get; set; }
    }
}