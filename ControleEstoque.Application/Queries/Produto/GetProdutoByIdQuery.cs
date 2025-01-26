using MediatR;
using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Application.Queries.Produto
{
    public class GetProdutoByIdQuery : IRequest<ControleEstoque.Domain.Entities.Produto?>
    {
        public int Id { get; set; }

        public GetProdutoByIdQuery(int id)
        {
            Id = id;
        }
    }
}
