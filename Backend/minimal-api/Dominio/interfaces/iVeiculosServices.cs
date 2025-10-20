using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using minimal_api.dominio.Entidades;

namespace minimal_api.dominio.interfaces
{
    public interface iVeiculosServices
    {
        Task<Veiculo> CriarAsync(Veiculo veiculo, CancellationToken ct = default);
        Task<IReadOnlyList<Veiculo>> ListarAsync(CancellationToken ct = default);
    }
}