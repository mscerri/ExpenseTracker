using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Dtos
{
    public class CreateCurrencyDto
    {
        [Required]
        public string Name { get; set; }
    }
}