using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure.Interfaces;
using ControleEstoque.Domain.Exceptions;

namespace ControleEstoque.Tests.ProdutoTests
{
    public class ProdutoReporTests
    {
        private readonly Mock<IProdutoCommandRepository> _produtoCommandRepositoryMock;
        private readonly ReporEstoqueCommandHandler _handler;

        public ProdutoReporTests()
        {
            _produtoCommandRepositoryMock = new Mock<IProdutoCommandRepository>();
            _handler = new ReporEstoqueCommandHandler(_produtoCommandRepositoryMock.Object);
        }

        [Fact]
        public async Task DeveReporEstoqueComSucesso()
        {
            // Arrange
            var command = new ReporEstoqueCommand(1, 10, 150.00m);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.ReporEstoqueAsync(1, 10, 150.00m))
                .ReturnsAsync(true);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            _produtoCommandRepositoryMock.Verify(repo => repo.ReporEstoqueAsync(1, 10, 150.00m), Times.Once);
        }

        [Fact]
        public async Task NaoDeveReporEstoqueComQuantidadeNegativa()
        {
            // Arrange
            var command = new ReporEstoqueCommand(1, -10, 150.00m);

            // Act & Assert
            await Assert.ThrowsAsync<EstoqueInsuficienteException>(async () =>
                await _handler.Handle(command, CancellationToken.None)
            );
        }

        [Fact]
        public async Task NaoDeveReporEstoqueComPrecoNegativo()
        {
            // Arrange
            var command = new ReporEstoqueCommand(1, 10, -150.00m);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _handler.Handle(command, CancellationToken.None)
            );
        }
    }
}
