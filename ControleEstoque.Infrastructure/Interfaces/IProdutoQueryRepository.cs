using ControleEstoque.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Infrastructure.Interfaces
{
    public interface IProdutoQueryRepository
    {
        Task<Produto?> ObterProdutoPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
    }
}
