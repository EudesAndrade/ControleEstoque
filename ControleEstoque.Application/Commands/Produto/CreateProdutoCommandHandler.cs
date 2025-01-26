using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure;
using MediatR;
using Dapper;

public class CreateProdutoCommandHandler : IRequestHandler<CreateProdutoCommand, int>
{
    private readonly IDapperContext _context;

    public CreateProdutoCommandHandler(IDapperContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
    {
        var connection = _context.CreateConnection();
        var query = "INSERT INTO Produtos (Nome, PartNumber, Quantidade, Preco) VALUES (@Nome, @PartNumber, @Quantidade, @Preco)";
        var parameters = new { request.Nome, request.PartNumber, request.Quantidade, request.Preco };

        var result = await connection.ExecuteScalarAsync<int>(query, parameters);
        return result;
    }
}
