using System.Data;
using MySqlConnector;  // Certifique-se que o pacote MySqlConnector está instalado
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

        public  IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
        //IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}
