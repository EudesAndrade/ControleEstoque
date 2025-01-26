using MediatR;

namespace ControleEstoque.Application.Commands.Produto
{
    public class UpdateProdutoCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string PartNumber { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public UpdateProdutoCommand() { }

        public UpdateProdutoCommand(int id, string nome, string partNumber, int quantidade, decimal preco)
        {
            Id = id;
            Nome = nome;
            PartNumber = partNumber;
            Quantidade = quantidade;
            Preco = preco;
        }
    }
}
