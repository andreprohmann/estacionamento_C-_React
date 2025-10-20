using Microsoft.EntityFrameworkCore;
using minimal_api.dominio.Entidades;

namespace minimal_api.infraestrutura.Db
{
    public class EstacionamentoContexto : DbContext
    {
        public EstacionamentoContexto(DbContextOptions<EstacionamentoContexto> options) : base(options) { }

        public DbSet<Veiculo> Veiculos => Set<Veiculo>();
        public DbSet<Vaga> Vagas => Set<Vaga>();
        public DbSet<Ocupacao> Ocupacoes => Set<Ocupacao>();

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Veiculo>(e =>
            {
                e.ToTable("veiculo");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Placa).IsRequired().HasMaxLength(10);
                e.HasIndex(x => x.Placa).IsUnique();
            });

            model.Entity<Vaga>(e =>
            {
                e.ToTable("vaga");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Numero).IsRequired().HasMaxLength(20);
                e.HasIndex(x => x.Numero).IsUnique();
                e.Property(x => x.Tipo).HasConversion<string>().HasMaxLength(20);
            });

            model.Entity<Ocupacao>(e =>
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
                e.Property(x => x.CheckOut).HasColumnType("datetime");
            });
        }
    }
}