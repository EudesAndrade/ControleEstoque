using MediatR;
using ControleEstoque.Infrastructure.Interfaces;

namespace ControleEstoque.Application.Commands.Produto
{
    public class ConsumirEstoqueCommandHandler : IRequestHandler<ConsumirEstoqueCommand, bool>
    {
        private readonly IProdutoCommandRepository _produtoCommandRepository;

        public ConsumirEstoqueCommandHandler(IProdutoCommandRepository produtoCommandRepository)
        {
            _produtoCommandRepository = produtoCommandRepository;
        }

        public async Task<bool> Handle(ConsumirEstoqueCommand request, CancellationToken cancellationToken)
        {
            return await _produtoCommandRepository.ConsumirEstoqueAsync(request.Id, request.Quantidade);
        }
    }
}
