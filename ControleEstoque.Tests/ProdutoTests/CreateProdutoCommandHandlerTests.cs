using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure;
using Dapper;
using Moq;
using Xunit;

namespace ControleEstoque.Tests.ProdutoTests
{
    public class CreateProdutoCommandHandlerTests
    {
        //[Fact]
        //public async Task Handle_DeveInserirProdutoERetornarId()
        //{
        //    // Arrange
        //    var command = new CreateProdutoCommand("Teclado", "TK-001", 10, 150.00m);

        //    // Criando mocks para IDbConnection e IDapperContext
        //    var mockConnection = new Mock<IDbConnection>();
        //    var mockDapperContext = new Mock<IDapperContext>();

        //    // Configuração do mock para criar a conexão simulada
        //    mockDapperContext
        //        .Setup(d => d.CreateConnection())
        //        .Returns(mockConnection.Object);

        //    // Configuração do mock para simular a execução do comando SQL e retornar um ID
        //    mockConnection
        //        .Setup(c => c.ExecuteScalarAsync<int>(
        //            It.IsAny<string>(),  // Qualquer string de consulta
        //            It.IsAny<object>(),  // Qualquer objeto de parâmetros
        //            null, null, null))   // Outros parâmetros nulos
        //        .ReturnsAsync(1);  // Simulando retorno do ID gerado

        //    // Criando instância do handler com o mock do contexto
        //    var handler = new CreateProdutoCommandHandler(mockDapperContext.Object);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.Equal(1, result);

        //    // Verificando se o método de conexão foi chamado apenas uma vez
        //    mockDapperContext.Verify(d => d.CreateConnection(), Times.Once);

        //    // Verificando se o método ExecuteScalarAsync foi chamado com os parâmetros corretos
        //    mockConnection.Verify(c =>
        //        c.ExecuteScalarAsync<int>(
        //            It.IsAny<string>(),
        //            It.IsAny<object>(),
        //            null, null, null),
        //        Times.Once);
        //}
    }
}
