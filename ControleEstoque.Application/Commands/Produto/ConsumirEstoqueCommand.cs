using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class ConsumirEstoqueCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}
