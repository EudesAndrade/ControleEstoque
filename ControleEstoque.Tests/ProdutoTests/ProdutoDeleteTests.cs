using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure.Interfaces;

namespace ControleEstoque.Tests.ProdutoTests
{
    public class ProdutoDeleteTests
    {
        private readonly Mock<IProdutoCommandRepository> _produtoCommandRepositoryMock;
        private readonly DeleteProdutoCommandHandler _handler;

        public ProdutoDeleteTests()
        {
            _produtoCommandRepositoryMock = new Mock<IProdutoCommandRepository>();
            _handler = new DeleteProdutoCommandHandler(_produtoCommandRepositoryMock.Object);
        }

        [Fact]
        public async Task DeveDeletarProdutoComSucesso()
        {
            // Arrange
            var command = new DeleteProdutoCommand(1);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.DeletarProdutoAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            _produtoCommandRepositoryMock.Verify(repo => repo.DeletarProdutoAsync(1), Times.Once);
        }

        [Fact]
        public async Task NaoDeveDeletarProdutoInexistente()
        {
            // Arrange
            var command = new DeleteProdutoCommand(99);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.DeletarProdutoAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task NaoDeveDeletarProdutoComIdNegativo()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new DeleteProdutoCommand(-1));

            Assert.Equal("O ID do produto deve ser maior que zero. (Parameter 'id')", exception.Message);
        }
    }
}
