using ControleEstoque.Infrastructure;
using System.Data;
using MySqlConnector;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviço de conexão ao banco de dados MySQL usando Dapper
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exemplo de endpoint para verificar a conexão com o banco de dados
app.MapGet("/test-connection", async (DapperContext dbContext) =>
{
    using var connection = dbContext.CreateConnection();
    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
    return result == 1 ? Results.Ok("Conexão com o banco de dados está funcionando!") : Results.Problem("Falha na conexão");
})
.WithName("TestDatabaseConnection")
.WithOpenApi();

app.Run();
