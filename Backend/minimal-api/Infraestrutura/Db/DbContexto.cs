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
    public DbSet<Vaga> Vagas { get; set; } = default!;
    public DbSet<Ocupacao> Ocupacoes => Set<Ocupacao>();
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    //Definir os dados iniciais ADMIN
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<admin>().HasData(
            new admin
            {
                Id = 1,
                Nome = "Admin",
                Email = "admin@example.com",
                Senha = "123456",
                Perfil = "Admin"
            }
        );
        
        modelBuilder.Entity<Veiculo>(e =>
        {
            e.ToTable("veiculo");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Placa).IsRequired().HasMaxLength(10);
            e.HasIndex(x => x.Placa).IsUnique();
        });

        modelBuilder.Entity<Vaga>(e =>
        {
            e.ToTable("vaga");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Numero).IsRequired().HasMaxLength(10);
            e.HasIndex(x => x.Numero).IsUnique();
            e.Property(x => x.Tipo).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<Ocupacao>(e =>
        {
            e.ToTable("ocupacao");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.HasOne(x => x.Vaga)
                .WithMany(s => s.Ocupacoes)
                .HasForeignKey(x => x.VagaId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Veiculo)
                .WithMany(v => v.Ocupacoes)
                .HasForeignKey(x => x.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => new { x.VagaId, x.CheckOut });
            e.HasIndex(x => new { x.VeiculoId, x.CheckOut });

            e.Property(x => x.CheckIn).HasColumnType("datetime");
            e.Property(x => x.CheckIn).HasColumnType("datetime");
        });
    }

    //Configurar o DbContexto
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        //Se o optionsBuilder não estiver configurado, 
        //configura com a string de conexão do appsettings.json
        if (!optionsBuilder.IsConfigured)
        {
            var stringConnection = _configuration.GetConnectionString("MySQl")?.ToString();
            //Se a string de conexão não for nula ou vazia, configura o DbContexto
            if (!string.IsNullOrEmpty(stringConnection))
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

