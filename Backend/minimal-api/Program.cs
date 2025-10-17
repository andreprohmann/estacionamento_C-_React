using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using minimal_api.infraestrutura.Db;
using minimal_api.dominio.interfaces;
using minimal_api.dominio.Servicos;
using minimal_api.DTOs;
using Microsoft.AspNetCore.Mvc;
using minimal_api.dominio.ModelViews;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<iAdminServices, adminServices>() ;

//Adicionar o serviço de veiculos
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurar o DbContexto com a string de conexão do appsettings.json
builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQl"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQl"))
    );
});

var app = builder.Build();

// Endpoints Home
app.MapGet("/", () => Results.Json(new Home()));

// Endpoint de login
app.MapPost("/login", ([FromBody]LoginDTO loginDTO, iAdminServices adminServices) =>
{
    // Verificar se o login é válido
    if (adminServices.Login(loginDTO) != null)
    {
        return Results.Ok("Login bem-sucedido");
    }
    else
    {
        return Results.Unauthorized();
    }
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Dashboard
app.MapGet("/estacionamento", () => "Estacionamento");

// Cadastro de Veículo
app.MapGet("/veiculo", () => "Veículo");

// Cadastro de funcionario
app.MapGet("/funcionario", () => "funcionario");

app.Run();

