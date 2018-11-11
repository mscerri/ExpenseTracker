using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTO
{
    public class CurrencyDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}