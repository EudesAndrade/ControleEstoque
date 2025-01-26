using MediatR;
using ControleEstoque.Infrastructure.Interfaces;

namespace ControleEstoque.Application.Commands.Produto
{
    public class UpdateProdutoCommandHandler : IRequestHandler<UpdateProdutoCommand, bool>
    {
        private readonly IProdutoCommandRepository _produtoCommandRepository;

        public UpdateProdutoCommandHandler(IProdutoCommandRepository produtoCommandRepository)
        {
            _produtoCommandRepository = produtoCommandRepository;
        }

        public async Task<bool> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = new ControleEstoque.Domain.Entities.Produto(request.Nome, request.PartNumber, request.Quantidade, request.Preco);
            produto.SetId(request.Id);

            return await _produtoCommandRepository.AtualizarProdutoAsync(produto);
        }
    }
}
