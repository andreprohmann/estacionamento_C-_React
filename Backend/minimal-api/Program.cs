#region Usings 
using Microsoft.EntityFrameworkCore;
using minimal_api.infraestrutura.Db;
using minimal_api.dominio.interfaces;
using minimal_api.dominio.Servicos;
using minimal_api.DTOs;
using Microsoft.AspNetCore.Mvc;
using minimal_api.dominio.ModelViews;
using minimal_api.dominio.DTOs;
using minimal_api.dominio.Entidades;
using MyNamespace.Dominio.ModelViews;
#endregion
#region Builder
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<iAdminServices, adminServices>() ;
builder.Services.AddScoped<iVeiculosServices, veiculosServices>();

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
#endregion
#region Home
// Endpoints Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home"); // ✅ certo
#endregion
#region Admin
// Endpoint de login
app.MapPost("/admin/login", ([FromBody] LoginDTO loginDTO, iAdminServices adminServices) =>
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
}).WithTags("Admin");
app.MapPost("/admin", ([FromBody] AdminDTO adminDTO, iAdminServices adminServices) =>
{

    var validacao = new ErrorValid()
    {
        Messagens = new List<string>()
    };

    if (adminDTO.Email == null || !adminDTO.Email.Contains("@"))
    {
        validacao.Messagens.Add("Email inválido.");
    }
    else
    {
        var adminExistente = adminServices.BuscarPorEmail(adminDTO.Email);
        if (adminExistente != null)
        {
            validacao.Messagens.Add("Este e-mail já está cadastrado.");
        }
    }
    if (adminDTO.Perfil == null || adminDTO.Perfil.Length < 3)
    {
        validacao.Messagens.Add("Perfil deve ter no mínimo 3 caracteres.");
    }
    if (adminDTO.Nome == null || adminDTO.Nome.Length < 3)
    {
        validacao.Messagens.Add("Nome deve ter no mínimo 3 caracteres.");
    }
    if (adminDTO.Senha == null || adminDTO.Senha.Length < 6)
    {
        validacao.Messagens.Add("Senha deve ter no mínimo 6 caracteres.");
    }
    if (validacao.Messagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }
    var admin = new admin
    {
        
    };
    adminServices.Cadastrar(admin);
    return Results.Created($"/admin/{admin.Id}", admin);

}).WithTags("Admin");
#endregion
#region Veiculo

ErrorValid validaDTO(veiculoDTO veiculoDTO)
{
    var mensagensErro = new ErrorValid();
        mensagensErro.Messagens = new List<string>();
    // Inicializa a lista de mensagens de erro
    if (string.IsNullOrEmpty(veiculoDTO.Nome))
    {
        mensagensErro.Messagens.Add("O campo Nome é obrigatório.");
    }
    if (string.IsNullOrEmpty(veiculoDTO.Marca))
    {
        mensagensErro.Messagens.Add("O campo Marca é obrigatório.");
    }
    if (string.IsNullOrEmpty(veiculoDTO.Placa))
    {
        mensagensErro.Messagens.Add("O campo Placa é obrigatório.");
    }
    if (veiculoDTO.Ano < 1930 || veiculoDTO.Ano > DateTime.Now.Year)
    {
        mensagensErro.Messagens.Add("Ano do Veiculo é invalido.");
    }
    return mensagensErro;
}

app.MapPost("/veiculo", ([FromBody] veiculoDTO veiculoDTO, iVeiculosServices veiculoServices) =>
{
    
    var validacao = validaDTO(veiculoDTO);
    if (validacao.Messagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }

    var veiculo = new veiculo
    {
        Nome = veiculoDTO.Nome,
        Placa = veiculoDTO.Placa,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano,
        checkIn = veiculoDTO.checkIn,
    };
    veiculoServices.Cadastrar(veiculo);

    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);

}).WithTags("Veiculo");
//Busca todos os veiculos
app.MapGet("/veiculo", ([FromQuery] int? page, iVeiculosServices veiculoServices) =>
{
    var veiculos = veiculoServices.Todos(page);
    return Results.Ok(veiculos);
}).WithTags("Veiculo");
//Busca veiculo por Id
app.MapGet("/veiculo/{Id}", ([FromRoute] int Id, iVeiculosServices veiculoServices) =>
{
    var veiculos = veiculoServices.BuscarPorId(Id);

    if (veiculos == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(veiculos);
    }

}).WithTags("Veiculo");
//Atualiza veiculo por Id
app.MapPut("/veiculo/{Id}", ([FromRoute] int Id, veiculoDTO veiculoDTO, iVeiculosServices veiculoServices) =>
{

    var validacao = validaDTO(veiculoDTO);
    if (validacao.Messagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }
    var veiculos = veiculoServices.BuscarPorId(Id);

    if (veiculos == null)
    {
        return Results.NotFound();
    }
    else
    {
        veiculos.Nome = veiculoDTO.Nome;
        veiculos.Placa = veiculoDTO.Placa;
        veiculos.Marca = veiculoDTO.Marca;
        veiculos.Ano = veiculoDTO.Ano;
        veiculos.checkIn = veiculoDTO.checkIn;
        veiculos.checkOut = veiculoDTO.checkOut;
    }
    veiculoServices.Atualizar(veiculos);
    return Results.Ok(veiculos);
}).WithTags("Veiculo");
//Atualiza checkOut por Id

app.MapPut("/veiculo/atualizarCheckOut/{Id}", (
    [FromRoute] int Id,
    [FromBody] veiculoDTO veiculoDTO,
    [FromServices] iVeiculosServices veiculoServices) =>
{
    try
    {
        var veiculos = veiculoServices.BuscarPorId(Id);

        if (veiculos == null)
        {
            return Results.NotFound();
        }

        // Atualiza o checkOut
        veiculos.checkOut = veiculoDTO.checkOut;

        // Calcula a diferença entre checkIn e checkOut
        if (veiculos.checkIn != null && veiculos.checkOut != null)
        {
            TimeSpan duracao = veiculos.checkOut - veiculos.checkIn;

            // Calcula o valor a pagar (arredondando para cima)
            double horas = Math.Ceiling(duracao.TotalHours);
            double valorPorHora = 15.0;
            double valorTotal = horas * valorPorHora;

            // Você pode salvar esse valor no objeto, se tiver um campo para isso
            veiculos.valorTotal = valorTotal;

            Console.WriteLine($"Duração: {horas} horas");
            Console.WriteLine($"Valor a pagar: R${valorTotal}");
        }

        veiculoServices.Atualizar(veiculos);
        return Results.Ok(veiculos);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao atualizar check-out: {ex.Message}");
        return Results.Problem("Ocorreu um erro ao processar a solicitação.");
    }
}).WithTags("Veiculo");

//Deleta veiculo por Id
app.MapDelete("/veiculo/{Id}", ([FromRoute] int Id, iVeiculosServices veiculoServices) =>
{
var veiculos = veiculoServices.BuscarPorId(Id);
    if (veiculos == null)
    {
        return Results.NotFound();
    }
    else
    {
        veiculoServices.Deletar(veiculos);
        return Results.Ok("Veículo deletado com sucesso");
    }
}).WithTags("Veiculo");
#endregion
#region APP
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion
