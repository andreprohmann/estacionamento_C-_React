using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using minimal_api.infraestrutura.Db;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

public class EstacionamentoContextoFactory : IDesignTimeDbContextFactory<EstacionamentoContexto>
{
    public EstacionamentoContexto CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EstacionamentoContexto>();
        var connectionString = "Server=localhost;port=3306;database=estacionamento;user=root;password=root;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        
        return new EstacionamentoContexto(optionsBuilder.Options);
    }
}