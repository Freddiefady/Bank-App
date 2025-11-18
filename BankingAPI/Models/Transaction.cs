namespace BankingAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
    }
}
