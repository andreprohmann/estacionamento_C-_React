using Microsoft.EntityFrameworkCore;
using minimal_api.dominio.Entidades;

namespace minimal_api.infraestrutura.Db;



public class DbContexto : DbContext
{
    //Definir a string de conexão
    private readonly IConfiguration _configuration;

    //Construtor que recebe a configuração
    public DbContexto(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //Definir as entidades do banco de dados
    public DbSet<admin> Admins { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        //Se o optionsBuilder não estiver configurado, 
        //configura com a string de conexão do appsettings.json
        if(!optionsBuilder.IsConfigured)
        {
            var stringConnection = _configuration.GetConnectionString("MySQl")?.ToString();
            //Se a string de conexão não for nula ou vazia, configura o DbContexto
            if(!string.IsNullOrEmpty(stringConnection))
            {
                optionsBuilder.UseMySql(
                    stringConnection,
                        ServerVersion.AutoDetect(
                            stringConnection
                    ));

                return;
            }
        }
        
        
    }
}
