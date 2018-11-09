using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Entities
{
    public class TransactionCategory
    {
        public int TransactionCategoryId { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
