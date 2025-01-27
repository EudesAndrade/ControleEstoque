using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ControleEstoque.Application.Commands.Produto;
using Dapper;
using ControleEstoque.Infrastructure.Interfaces;
using ControleEstoque.Domain.Entities;

public class CreateProdutoCommandHandler : IRequestHandler<CreateProdutoCommand, int>
{
    private readonly IProdutoCommandRepository _produtoRepository;

    public CreateProdutoCommandHandler(IProdutoCommandRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<int> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
    {
        var produto = new Produto(request.Nome, request.PartNumber, request.Quantidade, request.Preco);
        return await _produtoRepository.AdicionarProdutoAsync(produto);
    }
}
