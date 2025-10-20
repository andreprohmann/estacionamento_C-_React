using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.infraestrutura.Db;
using minimal_api.dominio.interfaces;
using minimal_api.dominio.Servicos;
using minimal_api.dominio.Entidades;

var builder = WebApplication.CreateBuilder(args);

// Connection String (appsettings.json -> "MySql")
var cs = builder.Configuration.GetConnectionString("MySql")
         ?? "server=localhost;port=3306;database=estacionamento;user=root;password=senha;charset=utf8mb4";

// EF Core + Pomelo MySQL
builder.Services.AddDbContext<EstacionamentoContexto>(opt =>
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));

// Serviços de domínio
builder.Services.AddScoped<iVagasServices, VagasServices>();
builder.Services.AddScoped<iVeiculosServices, veiculosServices>();
builder.Services.AddScoped<iAdminServices, adminServices>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ===== Endpoints de Vagas =====

// Ocupação atual
app.MapGet("/vagas/ocupacao-atual",
    async ([FromServices] iVagasServices serv) =>
{
    var itens = await serv.OcupacaoAtualAsync();
    return Results.Ok(itens.Select(x => new { vaga = x.NumeroVaga, placa = x.Placa }));
}).WithTags("Vagas");

// Ocupar
app.MapPost("/vagas/{vagaId:long}/ocupar/{veiculoId:long}",
    async ([FromRoute] long vagaId,
           [FromRoute] long veiculoId,
           [FromServices] iVagasServices serv) =>
{
    await serv.OcuparAsync(vagaId, veiculoId);
    return Results.NoContent();
}).WithTags("Vagas");

// Liberar
app.MapPost("/vagas/{vagaId:long}/liberar",
    async ([FromRoute] long vagaId,
           [FromServices] iVagasServices serv) =>
{
    await serv.LiberarAsync(vagaId);
    return Results.NoContent();
}).WithTags("Vagas");

// ===== Endpoints de Veículos (exemplos mínimos) =====
app.MapGet("/veiculos", async ([FromServices] iVeiculosServices serv) =>
    Results.Ok(await serv.ListarAsync()))
.WithTags("Veículos");

app.MapPost("/veiculos", async ([FromBody] Veiculo veiculo,
                                [FromServices] iVeiculosServices serv) =>
{
    var criado = await serv.CriarAsync(veiculo);
    return Results.Created($"/veiculos/{criado.Id}", criado);
}).WithTags("Veículos");

app.Run();