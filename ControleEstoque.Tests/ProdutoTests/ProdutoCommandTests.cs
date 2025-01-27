using Xunit;
using Moq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure.Interfaces;
using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Tests.ProdutoTests
{
    public class ProdutoCommandTests
    {
        private readonly Mock<IProdutoCommandRepository> _produtoCommandRepositoryMock;
        private readonly CreateProdutoCommandHandler _handler;

        public ProdutoCommandTests()
        {
            _produtoCommandRepositoryMock = new Mock<IProdutoCommandRepository>();

            _handler = new CreateProdutoCommandHandler(_produtoCommandRepositoryMock.Object);
        }

        [Fact]
        public async Task DeveCriarProdutoComSucesso()
        {
            // Arrange
            var command = new CreateProdutoCommand("Teclado Mecânico", "TK-001", 10, 150.00m);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.AdicionarProdutoAsync(It.IsAny<Produto>()))
                .ReturnsAsync(1); // Simula retorno de ID 1

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
            _produtoCommandRepositoryMock.Verify(repo => repo.AdicionarProdutoAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task NaoDeveCriarProdutoComNomeInvalido()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
        new CreateProdutoCommand("", "TK-001", 10, 150.00m));

            Assert.Equal("Nome do produto é obrigatório. (Parameter 'nome')", exception.Message);
        }
    }
}
