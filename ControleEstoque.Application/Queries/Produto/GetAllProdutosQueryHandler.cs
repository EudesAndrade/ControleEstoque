using MediatR;
using ControleEstoque.Infrastructure.Interfaces;
using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Application.Queries.Produto
{
    public class GetAllProdutosQueryHandler : IRequestHandler<GetAllProdutosQuery, IEnumerable<ControleEstoque.Domain.Entities.Produto>>
    {
        private readonly IProdutoQueryRepository _produtoQueryRepository;

        public GetAllProdutosQueryHandler(IProdutoQueryRepository produtoQueryRepository)
        {
            _produtoQueryRepository = produtoQueryRepository;
        }

        public async Task<IEnumerable<ControleEstoque.Domain.Entities.Produto>> Handle(GetAllProdutosQuery request, CancellationToken cancellationToken)
        {
            return await _produtoQueryRepository.ObterTodosProdutosAsync();
        }
    }
}
