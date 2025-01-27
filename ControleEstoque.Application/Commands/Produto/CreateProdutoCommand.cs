using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class CreateProdutoCommand : IRequest<int>
    {
        public string Nome { get; }
        public string PartNumber { get; }
        public int Quantidade { get; }
        public decimal Preco { get; }

        public CreateProdutoCommand(string nome, string partNumber, int quantidade, decimal preco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto é obrigatório.", nameof(nome));

            if (string.IsNullOrWhiteSpace(partNumber))
                throw new ArgumentException("Part number é obrigatório.", nameof(partNumber));

            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.", nameof(quantidade));

            if (preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.", nameof(preco));

            Nome = nome;
            PartNumber = partNumber;
            Quantidade = quantidade;
            Preco = preco;
        }
    }
}
