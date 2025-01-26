using MediatR;
using ControleEstoque.Infrastructure;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
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
        using var connection = _context.CreateConnection();
        var query = "INSERT INTO produtos (Nome, PartNumber, Quantidade, Preco) VALUES (@Nome, @PartNumber, @Quantidade, @Preco); SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(query, request);
    }
}
