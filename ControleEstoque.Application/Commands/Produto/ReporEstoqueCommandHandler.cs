using MediatR;
using ControleEstoque.Infrastructure.Interfaces;

namespace ControleEstoque.Application.Commands.Produto
{
    public class ReporEstoqueCommandHandler : IRequestHandler<ReporEstoqueCommand, bool>
    {
        private readonly IProdutoCommandRepository _produtoCommandRepository;

        public ReporEstoqueCommandHandler(IProdutoCommandRepository produtoCommandRepository)
        {
            _produtoCommandRepository = produtoCommandRepository;
        }

        public async Task<bool> Handle(ReporEstoqueCommand request, CancellationToken cancellationToken)
        {
            return await _produtoCommandRepository.ReporEstoqueAsync(request.Id, request.Quantidade, request.Preco);
        }
    }
}
