using MediatR;
using ControleEstoque.Infrastructure.Interfaces;

namespace ControleEstoque.Application.Commands.Produto
{
    public class DeleteProdutoCommandHandler : IRequestHandler<DeleteProdutoCommand, bool>
    {
        private readonly IProdutoCommandRepository _produtoCommandRepository;

        public DeleteProdutoCommandHandler(IProdutoCommandRepository produtoCommandRepository)
        {
            _produtoCommandRepository = produtoCommandRepository;
        }

        public async Task<bool> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
        {
            return await _produtoCommandRepository.DeletarProdutoAsync(request.Id);
        }
    }
}
