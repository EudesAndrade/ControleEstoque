using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Infrastructure.Repositories.Queries
{
    public class ProdutoQueryRepository : IProdutoQueryRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProdutoQueryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
        {
            var query = "SELECT * FROM produtos";
            return await _dbConnection.QueryAsync<Produto>(query);
        }

        public async Task<Produto?> ObterProdutoPorIdAsync(int id)
        {
            var query = "SELECT * FROM produtos WHERE id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Produto>(query, new { Id = id });
        }
    }
}
