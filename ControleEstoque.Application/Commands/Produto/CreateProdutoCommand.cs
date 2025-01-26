using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class CreateProdutoCommand : IRequest<int>
    {
        public string Nome { get; set; }
        public string PartNumber { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public CreateProdutoCommand(string nome, string partNumber, int quantidade, decimal preco)
        {
            Nome = nome;
            PartNumber = partNumber;
            Quantidade = quantidade;
            Preco = preco;
        }
    }
}
