
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using minimal_api.infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);

// ===== Serviços (sempre ANTES do Build) =====
const string CorsPolicy = "Frontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});
builder.Services.AddDbContext<EstacionamentoContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Estacionamento API", Version = "v1" });
});

var app = builder.Build();

// ===== Middlewares / Pipeline (sempre DEPOIS do Build) =====
app.UseCors(CorsPolicy);

app.UseSwagger();
app.UseSwaggerUI();

// ===== Endpoints de exemplo =====
app.MapGet("/vagas/ocupacao-atual", () =>
{
    var total = 50;
    var occupied = 12;
    var available = total - occupied;
    return Results.Ok(new { totalSpots = total, occupied, available });
});

app.MapGet("/veiculos", () =>
{
    var veiculos = new[]
    {
        new { placa = "ABC1D23", modelo = "Fiesta", cor = "Prata", nomeMotorista = "João" },
        new { placa = "DEF2G45", modelo = "Onix", cor = "Preto", nomeMotorista = (string?)null },
    };
    return Results.Ok(veiculos);
});

app.MapPost("/estacionamento/checkin", (CheckinDto dto) =>
{
    // Aqui você incluiria persistência etc.
    return Results.Ok(new { ok = true });
});

app.MapPost("/estacionamento/checkout", (CheckoutDto dto) =>
{
    // Regra fictícia
    var price = 15.0m;
    return Results.Ok(new { price, vehicle = new { placa = dto.Placa } });
});

app.Run();

// ===== DTOs =====
public record CheckinDto(string Placa, string? Modelo, string? Cor, string? NomeMotorista);
public record CheckoutDto(string Placa);
