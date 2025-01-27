using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class ConsumirEstoqueCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }

        public ConsumirEstoqueCommand(int id, int quantidade)
        {
            if (id <= 0)
                throw new ArgumentException("ID do produto deve ser maior que zero.", nameof(id));

            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

            Id = id;
            Quantidade = quantidade;
        }
    }
}
