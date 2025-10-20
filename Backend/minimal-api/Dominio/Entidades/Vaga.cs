
using minimal_api.dominio.enuns;

namespace minimal_api.dominio.Entidades
{
    public class Vaga
    {
        public long Id { get; set; }
        public string Numero { get; set; } = default!;      // identificador humano (ex.: A-01)
        public TipoVaga Tipo { get; set; } = TipoVaga.CARRO;
        public bool Ativa { get; set; } = true;

        public ICollection<Ocupacao> Ocupacoes { get; set; } = new List<Ocupacao>();
    }
}