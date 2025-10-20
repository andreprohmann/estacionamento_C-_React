
namespace minimal_api.dominio.Entidades
{
    public class Veiculo
    {
        public long Id { get; set; }
        public string Placa { get; set; } = default!;
        public string? Modelo { get; set; }
        public string? Cor { get; set; }

        // Navegação (histórico de ocupações)
        public ICollection<Ocupacao> Ocupacoes { get; set; } = new List<Ocupacao>();
    }
}