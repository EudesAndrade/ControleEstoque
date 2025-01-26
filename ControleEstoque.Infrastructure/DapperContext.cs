using System.Data;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace ControleEstoque.Infrastructure
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}
