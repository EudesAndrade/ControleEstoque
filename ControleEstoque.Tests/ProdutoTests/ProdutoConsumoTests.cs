using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure.Interfaces;

namespace ControleEstoque.Tests.ProdutoTests
{
    public class ProdutoConsumoTests
    {
        private readonly Mock<IProdutoCommandRepository> _produtoCommandRepositoryMock;
        private readonly ConsumirEstoqueCommandHandler _handler;

        public ProdutoConsumoTests()
        {
            _produtoCommandRepositoryMock = new Mock<IProdutoCommandRepository>();
            _handler = new ConsumirEstoqueCommandHandler(_produtoCommandRepositoryMock.Object);
        }

        [Fact]
        public async Task DeveConsumirEstoqueComSucesso()
        {
            // Arrange
            var command = new ConsumirEstoqueCommand(1, 5);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.ConsumirEstoqueAsync(1, 5))
                .ReturnsAsync(true);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            _produtoCommandRepositoryMock.Verify(repo => repo.ConsumirEstoqueAsync(1, 5), Times.Once);
        }

        [Fact]
        public async Task NaoDeveConsumirEstoqueInsuficiente()
        {
            // Arrange
            var command = new ConsumirEstoqueCommand(1, 50);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.ConsumirEstoqueAsync(1, 50))
                .ReturnsAsync(false);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task NaoDeveConsumirQuantidadeNegativa()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new ConsumirEstoqueCommand(1, -5));

            Assert.Equal("A quantidade deve ser maior que zero. (Parameter 'quantidade')", exception.Message);
        }
    }
}
