using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using minimal_api.infraestrutura.Db;



var builder = WebApplication.CreateBuilder(args);

//Configurar o DbContexto com a string de conexão do appsettings.json
builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQl"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQl"))
    );
});

var app = builder.Build();

// Endpoint de login
app.MapPost("/login", (minimal_api.DTOs.LoginDTO loginDTO) =>
{
    if (loginDTO.Email == "admin@example.com" && loginDTO.Senha == "123456")
    {
        return Results.Ok("Login bem-sucedido");
    }
    else
    {
        return Results.Unauthorized();
    }
});

// Dashboard
app.MapGet("/estacionamento", () => "Estacionamento");

// Cadastro de Veículo
app.MapGet("/veiculo", () => "Veículo");

// Cadastro de funcionario
app.MapGet("/funcionario", () => "funcionario");

app.Run();

