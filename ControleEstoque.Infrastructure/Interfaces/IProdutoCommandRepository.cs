using ControleEstoque.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Infrastructure.Interfaces
{
    public interface IProdutoCommandRepository
    {
        Task<int> AdicionarProdutoAsync(Produto produto);
        Task<bool> AtualizarProdutoAsync(Produto produto);
        Task<bool> DeletarProdutoAsync(int id);
        Task<bool> ConsumirEstoqueAsync(int id, int quantidade);
        Task<bool> ReporEstoqueAsync(int id, int quantidade, decimal preco);
    }
}
