using ControleEstoque.Domain.Exceptions;

namespace ControleEstoque.Domain.Entities
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string PartNumber { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Preco { get; private set; }

        public Produto(string nome, string partNumber, int quantidade, decimal preco)
        {
            if (quantidade < 0)
                throw new EstoqueInsuficienteException("A quantidade não pode ser negativa.");

            Nome = nome;
            PartNumber = partNumber;
            Quantidade = quantidade;
            Preco = preco;
        }

        public void AtualizarPreco(decimal novoPreco)
        {
            if (novoPreco <= 0)
                throw new ArgumentException("O preço deve ser maior que zero.");

            Preco = novoPreco;
        }
    }
}
