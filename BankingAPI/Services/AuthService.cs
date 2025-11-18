using Microsoft.Data.SqlClient;
using BankingAPI.Models;

namespace BankingAPI.Services
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");
        }

        /// Authenticates a user with username and password
        public async Task<User?> AuthenticateUser(string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_AuthenticateUser", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId = reader.GetInt32(0),
                    Username = reader.GetString(1)
                };
            }

            return null;
        }
    }
}
