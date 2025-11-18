using Microsoft.Data.SqlClient;
using BankingAPI.Models;
using BankingAPI.DTOs;

namespace BankingAPI.Services
{
    public class TransactionService
    {
        private readonly string _connectionString;

        public TransactionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");
        }

        /// <summary>
        /// Records a new transaction (deposit or withdrawal) for a client
        /// </summary>
        public async Task<Transaction> RecordTransaction(CreateTransactionDto transactionDto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_RecordTransaction", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ClientId", transactionDto.ClientId);
            command.Parameters.AddWithValue("@TransactionType", transactionDto.TransactionType);
            command.Parameters.AddWithValue("@TransactionAmount", transactionDto.TransactionAmount);
            command.Parameters.AddWithValue("@Description", (object?)transactionDto.Description ?? DBNull.Value);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Transaction
                {
                    TransactionId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    TransactionType = reader.GetString(2),
                    TransactionAmount = reader.GetDecimal(3),
                    BalanceAfterTransaction = reader.GetDecimal(4),
                    TransactionDate = reader.GetDateTime(5),
                    Description = reader.IsDBNull(6) ? null : reader.GetString(6)
                };
            }

            throw new Exception("Failed to record transaction");
        }

        /// <summary>
        /// Retrieves all transactions from the database, ordered by date (newest first)
        /// </summary>
        public async Task<List<Transaction>> GetAllTransactions()
        {
            var transactions = new List<Transaction>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GetAllTransactions", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                transactions.Add(new Transaction
                {
                    TransactionId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    ClientName = reader.GetString(2),
                    AccountNumber = reader.GetString(3),
                    TransactionType = reader.GetString(4),
                    TransactionAmount = reader.GetDecimal(5),
                    BalanceAfterTransaction = reader.GetDecimal(6),
                    TransactionDate = reader.GetDateTime(7),
                    Description = reader.IsDBNull(8) ? null : reader.GetString(8)
                });
            }

            return transactions;
        }

        /// <summary>
        /// Retrieves all transactions for a specific client
        /// </summary>
        public async Task<List<Transaction>> GetTransactionsByClientId(int clientId)
        {
            var transactions = new List<Transaction>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GetTransactionsByClientId", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ClientId", clientId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                transactions.Add(new Transaction
                {
                    TransactionId = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    ClientName = reader.GetString(2),
                    AccountNumber = reader.GetString(3),
                    TransactionType = reader.GetString(4),
                    TransactionAmount = reader.GetDecimal(5),
                    BalanceAfterTransaction = reader.GetDecimal(6),
                    TransactionDate = reader.GetDateTime(7),
                    Description = reader.IsDBNull(8) ? null : reader.GetString(8)
                });
            }

            return transactions;
        }

        /// <summary>
        /// Gets transaction statistics for a specific client
        /// </summary>
        public async Task<TransactionSummary> GetClientTransactionSummary(int clientId)
        {
            var transactions = await GetTransactionsByClientId(clientId);

            return new TransactionSummary
            {
                TotalTransactions = transactions.Count,
                TotalDeposits = transactions.Where(t => t.TransactionType == "Deposit").Sum(t => t.TransactionAmount),
                TotalWithdrawals = transactions.Where(t => t.TransactionType == "Withdraw").Sum(t => t.TransactionAmount),
                LastTransactionDate = transactions.OrderByDescending(t => t.TransactionDate).FirstOrDefault()?.TransactionDate
            };
        }
    }

    /// <summary>
    /// Summary of transaction statistics for a client
    /// </summary>
    public class TransactionSummary
    {
        public int TotalTransactions { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }
        public DateTime? LastTransactionDate { get; set; }
    }
}
