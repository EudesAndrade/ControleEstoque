using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class ReporEstoqueCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
