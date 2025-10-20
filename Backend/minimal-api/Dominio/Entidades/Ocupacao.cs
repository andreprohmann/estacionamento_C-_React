using System;

namespace minimal_api.dominio.Entidades
{
    public class Ocupacao
    {
        public long Id { get; set; }
        public long VagaId { get; set; }
        public long VeiculoId { get; set; }
        public DateTime CheckIn { get; set; } = DateTime.UtcNow;
        public DateTime? CheckOut { get; set; }

        public Vaga Vaga { get; set; } = default!;
        public Veiculo Veiculo { get; set; } = default!;
    }
}