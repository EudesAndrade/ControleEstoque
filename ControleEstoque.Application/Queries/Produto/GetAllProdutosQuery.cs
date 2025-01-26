using MediatR;
using ControleEstoque.Domain.Entities;
using System.Collections.Generic;

namespace ControleEstoque.Application.Queries.Produto
{
    public class GetAllProdutosQuery : IRequest<IEnumerable<ControleEstoque.Domain.Entities.Produto>>
    {
    }
}
