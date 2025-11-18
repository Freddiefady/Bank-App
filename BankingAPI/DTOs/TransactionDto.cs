using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class CreateTransactionDto
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        [RegularExpression("^(Deposit|Withdraw)$", ErrorMessage = "Transaction type must be 'Deposit' or 'Withdraw'")]
        public string TransactionType { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Transaction amount must be greater than 0")]
        public decimal TransactionAmount { get; set; }

        public string? Description { get; set; }
    }
}
