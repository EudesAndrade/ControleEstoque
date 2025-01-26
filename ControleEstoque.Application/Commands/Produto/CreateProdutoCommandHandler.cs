using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using Dapper;
using ControleEstoque.Infrastructure.Interfaces;

public class CreateProdutoCommandHandler : IRequestHandler<CreateProdutoCommand, int>
{
    private readonly IDapperContext _context;

    public CreateProdutoCommandHandler(IDapperContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.PartNumber))
            throw new ArgumentException("O PartNumber é obrigatório.");

        if (request.Quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (request.Preco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        using var connection = _context.CreateConnection();
        var query = "INSERT INTO produtos (Nome, PartNumber, Quantidade, Preco) VALUES (@Nome, @PartNumber, @Quantidade, @Preco); SELECT LAST_INSERT_ID();";

        return await connection.ExecuteScalarAsync<int>(query, request);
    }
}
