using System.Data;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using ControleEstoque.Infrastructure.Interfaces;

namespace ControleEstoque.Infrastructure.Context
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
