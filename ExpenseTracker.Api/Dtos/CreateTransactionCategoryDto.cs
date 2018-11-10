using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Dtos
{
    public class CreateTransactionCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}