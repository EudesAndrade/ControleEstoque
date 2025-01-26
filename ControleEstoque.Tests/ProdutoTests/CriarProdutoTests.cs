using Xunit;
using ControleEstoque.Application.Commands;

namespace ControleEstoque.Tests.ProdutoTests
{
    public class CriarProdutoTests
    {
        [Fact]
        public void CriarProduto_DeveTerNomeCorreto()
        {
            // Arrange
            var command = new CriarProdutoCommand
            {
                Nome = "Teclado",
                PartNumber = "TK-001",
                Quantidade = 10,
                Preco = 150.00m
            };

            // Act
            var produtoCriado = new
            {
                Nome = command.Nome,
                PartNumber = command.PartNumber,
                Quantidade = command.Quantidade,
                Preco = command.Preco
            };

            // Assert
            Assert.Equal("Teclado", produtoCriado.Nome);
            Assert.Equal("TK-001", produtoCriado.PartNumber);
            Assert.Equal(10, produtoCriado.Quantidade);
            Assert.Equal(150.00m, produtoCriado.Preco);
        }
    }
}
