using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Interfaces;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace ControleEstoque.Infrastructure.Repositories
{
    public class LogErroRepository : ILogErroRepository
    {
        private readonly IDbConnection _dbConnection;

        public LogErroRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task RegistrarLogAsync(LogErro log)
        {
            var query = "INSERT INTO LogErro (Mensagem, StackTrace, DataHora) VALUES (@Mensagem, @StackTrace, @DataHora)";
            await _dbConnection.ExecuteAsync(query, log);
        }
    }
}
