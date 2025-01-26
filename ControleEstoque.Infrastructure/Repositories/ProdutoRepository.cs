using ControleEstoque.Infrastructure.Repositories.Interfaces;
using ControleEstoque.Domain.Entities;
using Dapper;
using System.Data;
using System.Data.Common;

namespace ControleEstoque.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProdutoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AdicionarProdutoAsync(Produto produto)
        {
            var query = @"INSERT INTO produtos (nome, partNumber, quantidade, preco) 
                  VALUES (@Nome, @PartNumber, @Quantidade, @Preco);
                  SELECT LAST_INSERT_ID();";

            var id = await _dbConnection.ExecuteScalarAsync<int>(query, new
            {
                produto.Nome,
                produto.PartNumber,
                produto.Quantidade,
                produto.Preco
            });

            return id;
        }

        public async Task<bool> AtualizarProdutoAsync(Produto produto)
        {
            var query = @"UPDATE produtos 
                          SET Nome = @Nome, partNumber = @PartNumber, Quantidade = @Quantidade, Preco = @Preco 
                          WHERE ID = @Id";

            var result = await _dbConnection.ExecuteAsync(query, produto);
            return result > 0;
        }

        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var query = "DELETE FROM produtos WHERE ID = @Id";
            var result = await _dbConnection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }

        public async Task<Produto?> ObterProdutoPorIdAsync(int id)
        {
            var query = "SELECT * FROM produtos WHERE ID = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Produto>(query, new { Id = id });
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
        {
            var query = "SELECT id, nome, partnumber, quantidade, preco FROM produtos";
            return await _dbConnection.QueryAsync<Produto>(query);
        }
    }
}
