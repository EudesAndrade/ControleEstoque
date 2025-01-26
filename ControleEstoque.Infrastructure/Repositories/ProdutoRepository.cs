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
            var verificarQuery = "SELECT COUNT(1) FROM produtos WHERE partNumber = @PartNumber";
            var existe = await _dbConnection.ExecuteScalarAsync<int>(verificarQuery, new { produto.PartNumber });

            if (existe > 0)
            {
                throw new Exception("Já existe um produto cadastrado com este PartNumber.");
            }

            var query = @"INSERT INTO produtos (nome, partNumber, quantidade, preco, custoTotal) 
                  VALUES (@Nome, @PartNumber, @Quantidade, @Preco, @CustoTotal);
                  SELECT LAST_INSERT_ID();";

            produto.CustoTotal = produto.Quantidade * produto.Preco;

            var id = await _dbConnection.ExecuteScalarAsync<int>(query, new
            {
                produto.Nome,
                produto.PartNumber,
                produto.Quantidade,
                produto.Preco,
                produto.CustoTotal
            });

            return id;
        }

        public async Task<bool> AtualizarProdutoAsync(Produto produto)
        {
            var query = @"UPDATE produtos 
                  SET nome = @Nome, 
                      partNumber = @PartNumber, 
                      quantidade = @Quantidade, 
                      preco = @Preco, 
                      custoTotal = @CustoTotal
                  WHERE id = @Id";

            produto.CustoTotal = produto.Quantidade * produto.Preco; // Recalcula o custo total na atualização

            var result = await _dbConnection.ExecuteAsync(query, new
            {
                produto.Id,
                produto.Nome,
                produto.PartNumber,
                produto.Quantidade,
                produto.Preco,
                produto.CustoTotal
            });

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
            var query = "SELECT id, nome, partNumber AS PartNumber, quantidade, preco, custoTotal FROM produtos";
            return await _dbConnection.QueryAsync<Produto>(query);
        }

        public async Task<bool> ConsumirEstoqueAsync(int id, int quantidade)
        {
            var query = @"UPDATE produtos 
                  SET quantidade = quantidade - @Quantidade 
                  WHERE id = @Id AND quantidade >= @Quantidade";

            var result = await _dbConnection.ExecuteAsync(query, new { Id = id, Quantidade = quantidade });
            return result > 0;
        }

        public async Task<bool> ReporEstoqueAsync(int id, int quantidade, decimal preco)
        {
            var query = @"UPDATE produtos 
                  SET quantidade = quantidade + @Quantidade, 
                      custoTotal = ((quantidade * preco) + (@Quantidade * @Preco)) / (quantidade + @Quantidade),
                      preco = @Preco
                  WHERE id = @Id";

            var result = await _dbConnection.ExecuteAsync(query, new { Id = id, Quantidade = quantidade, Preco = preco });
            return result > 0;
        }
    }
}
