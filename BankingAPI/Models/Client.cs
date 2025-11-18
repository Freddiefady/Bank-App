namespace BankingAPI.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public int Age { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal MaxCreditBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
