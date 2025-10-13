var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// Login
app.MapGet("/", () => "Hello World!");


// Dashboard
app.MapGet("/estacionamento", () => "Estacionamento");

// Cadastro de Veículo

app.MapGet("/veiculo", () => "Veículo");

// Cadastro de funcionario
app.MapGet("/funcionario", () => "funcionario");

app.Run();
