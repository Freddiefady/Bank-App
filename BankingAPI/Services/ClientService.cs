using Microsoft.Data.SqlClient;
using BankingAPI.Models;
using BankingAPI.DTOs;

namespace BankingAPI.Services
{
    public class ClientService
    {
        private readonly string _connectionString;

        public ClientService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");
        }

        /// <summary>
        /// Adds a new client to the database
        /// </summary>
        public async Task<int> AddClient(CreateClientDto clientDto)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_AddClient", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", clientDto.Name);
            command.Parameters.AddWithValue("@NationalId", clientDto.NationalId);
            command.Parameters.AddWithValue("@Age", clientDto.Age);
            command.Parameters.AddWithValue("@AccountNumber", clientDto.AccountNumber);
            command.Parameters.AddWithValue("@MaxCreditBalance", clientDto.MaxCreditBalance);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Retrieves all active clients with their current balances
        /// </summary>
        public async Task<List<Client>> GetAllClients()
        {
            var clients = new List<Client>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GetAllClients", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                clients.Add(new Client
                {
                    ClientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    NationalId = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    AccountNumber = reader.GetString(4),
                    MaxCreditBalance = reader.GetDecimal(5),
                    CurrentBalance = reader.GetDecimal(6),
                    CreatedDate = reader.GetDateTime(7),
                    IsActive = reader.GetBoolean(8)
                });
            }

            return clients;
        }

        /// <summary>
        /// Retrieves a specific client by their ID
        /// </summary>
        public async Task<Client?> GetClientById(int clientId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GetClientById", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ClientId", clientId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Client
                {
                    ClientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    NationalId = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    AccountNumber = reader.GetString(4),
                    MaxCreditBalance = reader.GetDecimal(5),
                    CurrentBalance = reader.GetDecimal(6),
                    CreatedDate = reader.GetDateTime(7),
                    IsActive = reader.GetBoolean(8)
                };
            }

            return null;
        }

        /// <summary>
        /// Validates if a client exists in the database
        /// </summary>
        public async Task<bool> ClientExists(int clientId)
        {
            var client = await GetClientById(clientId);
            return client != null;
        }
    }
}
