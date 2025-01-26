using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class DeleteProdutoCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteProdutoCommand(int id)
        {
            Id = id;
        }
    }
}
