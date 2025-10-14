using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Login
app.MapGet("/", () => "Hello World!");

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

