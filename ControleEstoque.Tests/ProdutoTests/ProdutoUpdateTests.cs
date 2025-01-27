using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure.Interfaces;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Domain.Exceptions;

namespace ControleEstoque.Tests.ProdutoTests
{
    public class ProdutoUpdateTests
    {
        private readonly Mock<IProdutoCommandRepository> _produtoCommandRepositoryMock;
        private readonly UpdateProdutoCommandHandler _handler;

        public ProdutoUpdateTests()
        {
            _produtoCommandRepositoryMock = new Mock<IProdutoCommandRepository>();
            _handler = new UpdateProdutoCommandHandler(_produtoCommandRepositoryMock.Object);
        }

        [Fact]
        public async Task DeveAtualizarProdutoComSucesso()
        {
            // Arrange
            var command = new UpdateProdutoCommand(1, "Mouse Gamer", "MG-002", 30, 250.00m);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.AtualizarProdutoAsync(It.IsAny<Produto>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _produtoCommandRepositoryMock.Verify(repo => repo.AtualizarProdutoAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task NaoDeveAtualizarProdutoInexistente()
        {
            // Arrange
            var command = new UpdateProdutoCommand(99, "Teclado", "TK-999", 20, 200.00m);

            _produtoCommandRepositoryMock
                .Setup(repo => repo.AtualizarProdutoAsync(It.IsAny<Produto>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task NaoDeveAtualizarProdutoComQuantidadeNegativa()
        {
            // Arrange
            var command = new UpdateProdutoCommand(1, "Mouse Gamer", "MG-002", -10, 250.00m);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EstoqueInsuficienteException>(async () =>
                await _handler.Handle(command, CancellationToken.None)
            );

            Assert.Equal("A quantidade não pode ser negativa.", exception.Message);
        }
    }
}
