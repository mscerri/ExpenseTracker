using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTO
{
    public class CreateCurrencyDto
    {
        [Required]
        public string Name { get; set; }
    }
}