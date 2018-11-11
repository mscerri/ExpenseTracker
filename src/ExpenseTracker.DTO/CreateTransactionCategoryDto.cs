using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTO
{
    public class CreateTransactionCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}