using ControleEstoque.Infrastructure;
using System.Data;
using MySqlConnector;
using Dapper;
using MediatR;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure.Repositories;
using ControleEstoque.Infrastructure.Repositories.Interfaces;
using ControleEstoque.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Registro de serviços

// Registro do contexto Dapper e conexão com o banco de dados
builder.Services.AddSingleton<IDapperContext, DapperContext>();
builder.Services.AddScoped<IDbConnection>(sp => sp.GetRequiredService<IDapperContext>().CreateConnection());

// Registro do MediatR para CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateProdutoCommand).Assembly));

// Registro do repositório
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Controle Estoque API",
        Version = "v1"
    });

    c.OperationFilter<SwaggerParameterDescriptionFilter>();
});

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint para listar produtos
app.MapGet("/produtos", async (IProdutoRepository produtoRepo) =>
{
    var produtos = await produtoRepo.ObterTodosProdutosAsync();
    return produtos.Any() ? Results.Ok(produtos) : Results.NotFound("Nenhum produto encontrado.");
});

// Endpoint para adicionar um novo produto
app.MapPost("/produtos", async (ProdutoCreateDto produtoDto, IProdutoRepository produtoRepo) =>
{
    // Criando uma instância da entidade Produto com os dados do DTO
    var produto = new Produto(produtoDto.Nome, produtoDto.PartNumber, produtoDto.Quantidade, produtoDto.Preco);

    // Adicionando o produto ao banco de dados
    var id = await produtoRepo.AdicionarProdutoAsync(produto);

    // Retornando o resultado com a URL do novo produto criado
    return Results.Created($"/produtos/{id}", produto);
});


// Endpoint para atualizar um produto existente
app.MapPut("/produtos/{id}", async (int id, Produto produto, IProdutoRepository produtoRepo) =>
{
    if (id != produto.Id)
        return Results.BadRequest("ID do produto não corresponde.");

    if (id <= 0)
    {
        return Results.BadRequest("O ID do produto é obrigatório e deve ser maior que zero.");
    }

    var resultado = await produtoRepo.AtualizarProdutoAsync(produto);
    return resultado ? Results.Ok("Produto atualizado com sucesso.") : Results.NotFound("Produto não encontrado.");
});


// Endpoint para deletar um produto
app.MapDelete("/produtos/{id}", async (int id, IProdutoRepository produtoRepo) =>
{
    var removido = await produtoRepo.DeletarProdutoAsync(id);
    return removido ? Results.Ok("Produto removido com sucesso.") : Results.NotFound("Produto não encontrado.");
});

// Endpoint para consumir estoque de um produto específico.
app.MapPut("/produtos/{id}/consumir", async (int id, int quantidade, IProdutoRepository produtoRepo) =>
{
    var sucesso = await produtoRepo.ConsumirEstoqueAsync(id, quantidade);
    return sucesso ? Results.Ok("Estoque atualizado com sucesso.") : Results.BadRequest("Estoque insuficiente.");
});

// Endpoint para repor estoque de um produto específico.
// O preço médio é atualizado automaticamente com base no novo custo.
app.MapPut("/produtos/{id}/repor", async (int id, int quantidade, decimal preco, IProdutoRepository produtoRepo) =>
{
    var sucesso = await produtoRepo.ReporEstoqueAsync(id, quantidade, preco);
    return sucesso ? Results.Ok("Estoque reposto com sucesso.") : Results.BadRequest("Erro ao repor estoque.");
});

// Endpoint para testar conexão com o banco
app.MapGet("/test-connection", async (IDapperContext dbContext) =>
{
    using var connection = dbContext.CreateConnection();
    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
    return result == 1 ? Results.Ok("Conexão com o banco de dados está funcionando!") : Results.Problem("Falha na conexão");
})
.WithName("TestDatabaseConnection")
.WithOpenApi();

app.Run();
