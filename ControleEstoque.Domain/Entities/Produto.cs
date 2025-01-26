using ControleEstoque.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleEstoque.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O part number é obrigatório.")]
        public string PartNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        // Construtor padrão exigido pelo Dapper
        public Produto() {

            Nome = string.Empty;
            PartNumber = string.Empty;
            Quantidade = 0;
            Preco = 0m;
        }

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
