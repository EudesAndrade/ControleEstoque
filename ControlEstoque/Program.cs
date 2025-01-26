using System.Data;
using MySqlConnector;
using Dapper;
using MediatR;
using ControleEstoque.Application.Commands.Produto;
using ControleEstoque.Infrastructure.Repositories;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Interfaces;
using ControleEstoque.Infrastructure.Context;
using ControleEstoque.Infrastructure.Repositories.Commands;
using ControleEstoque.Infrastructure.Repositories.Queries;
using ControleEstoque.Application.Queries.Produto;

var builder = WebApplication.CreateBuilder(args);

// Registro de serviços

// Registro do contexto Dapper e conexão com o banco de dados
builder.Services.AddSingleton<IDapperContext, DapperContext>();
builder.Services.AddScoped<IDbConnection>(sp => sp.GetRequiredService<IDapperContext>().CreateConnection());

// Registro dos repositórios
builder.Services.AddScoped<IProdutoCommandRepository, ProdutoCommandRepository>();
builder.Services.AddScoped<IProdutoQueryRepository, ProdutoQueryRepository>();
builder.Services.AddScoped<ILogErroRepository, LogErroRepository>();

// Registro do MediatR para CQRS com LoggingBehavior
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateProdutoCommand).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ControleEstoque.Application.Behaviors.LoggingBehavior<,>));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

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
app.MapGet("/produtos", async (IMediator mediator) =>
{
    var produtos = await mediator.Send(new GetAllProdutosQuery());
    return produtos.Any() ? Results.Ok(produtos) : Results.NotFound("Nenhum produto encontrado.");
});

// Endpoint para adicionar um novo produto
app.MapPost("/produtos", async (CreateProdutoCommand command, IMediator mediator) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/produtos/{id}", command);
});

// Endpoint para atualizar um produto existente
app.MapPut("/produtos/{id}", async (int id, UpdateProdutoCommand command, IMediator mediator) =>
{
    if (id != command.Id)
        return Results.BadRequest("ID do produto não corresponde.");

    var resultado = await mediator.Send(command);
    return resultado ? Results.Ok("Produto atualizado com sucesso.") : Results.NotFound("Produto não encontrado.");
});

// Endpoint para deletar um produto
app.MapDelete("/produtos/{id}", async (int id, IMediator mediator) =>
{
    var removido = await mediator.Send(new DeleteProdutoCommand(id));
    return removido ? Results.Ok("Produto removido com sucesso.") : Results.NotFound("Produto não encontrado.");
});

// Endpoint para consumir estoque de um produto específico.
app.MapPut("/produtos/{id}/consumir", async (int id, int quantidade, IMediator mediator) =>
{
    var command = new ConsumirEstoqueCommand { Id = id, Quantidade = quantidade };
    var sucesso = await mediator.Send(command);
    return sucesso ? Results.Ok("Estoque atualizado com sucesso.") : Results.BadRequest("Estoque insuficiente.");
});

// Endpoint para repor estoque de um produto específico.
// O preço médio é atualizado automaticamente com base no novo custo.
app.MapPut("/produtos/{id}/repor", async (int id, int quantidade, decimal preco, IMediator mediator) =>
{
    var command = new ReporEstoqueCommand { Id = id, Quantidade = quantidade, Preco = preco };
    var sucesso = await mediator.Send(command);
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
