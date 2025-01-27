using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class ReporEstoqueCommand : IRequest<bool>
    {
        public int Id { get; }
        public int Quantidade { get; }
        public decimal Preco { get; }

        public ReporEstoqueCommand(int id, int quantidade, decimal preco)
        {
            if (id <= 0)
                throw new ArgumentException("O ID do produto deve ser maior que zero.", nameof(id));

            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

            if (preco <= 0)
                throw new ArgumentException("O preço deve ser maior que zero.", nameof(preco));

            Id = id;
            Quantidade = quantidade;
            Preco = preco;
        }
    }
}
