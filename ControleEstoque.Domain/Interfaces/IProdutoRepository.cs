using System.Collections.Generic;
using System.Threading.Tasks;
using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> ObterPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(int id);
    }
}
