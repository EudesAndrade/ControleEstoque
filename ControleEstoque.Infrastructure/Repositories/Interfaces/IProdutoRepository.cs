using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Infrastructure.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<int> AdicionarProdutoAsync(Produto produto);
        Task<bool> AtualizarProdutoAsync(Produto produto);
        Task<bool> DeletarProdutoAsync(int id);
        Task<Produto?> ObterProdutoPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
        Task<bool> ConsumirEstoqueAsync(int id, int quantidade);
        Task<bool> ReporEstoqueAsync(int id, int quantidade, decimal preco);
    }
}
