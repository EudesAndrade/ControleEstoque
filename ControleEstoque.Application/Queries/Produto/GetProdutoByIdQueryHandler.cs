using MediatR;
using ControleEstoque.Infrastructure.Interfaces;
using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Application.Queries.Produto
{
    public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery, ControleEstoque.Domain.Entities.Produto?>
    {
        private readonly IProdutoQueryRepository _produtoQueryRepository;

        public GetProdutoByIdQueryHandler(IProdutoQueryRepository produtoQueryRepository)
        {
            _produtoQueryRepository = produtoQueryRepository;
        }

        public async Task<ControleEstoque.Domain.Entities.Produto?> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _produtoQueryRepository.ObterProdutoPorIdAsync(request.Id);
        }
    }
}
