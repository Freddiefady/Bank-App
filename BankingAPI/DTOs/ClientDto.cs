using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class CreateClientDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string NationalId { get; set; } = string.Empty;

        [Required]
        [Range(18, 120)]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MaxCreditBalance { get; set; }
    }
}
