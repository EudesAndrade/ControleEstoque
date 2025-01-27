using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class DeleteProdutoCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteProdutoCommand(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID do produto deve ser maior que zero.", nameof(id));
            }
            Id = id;
        }
    }
}
