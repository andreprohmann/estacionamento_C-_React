using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace minimal_api.dominio.interfaces
{
    public interface iVagasServices
    {
        Task OcuparAsync(long vagaId, long veiculoId, CancellationToken ct = default);
        Task LiberarAsync(long vagaId, CancellationToken ct = default);
        Task<List<(string NumeroVaga, string? Placa)>> OcupacaoAtualAsync(CancellationToken ct = default);
    }
}