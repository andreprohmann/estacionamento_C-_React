using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.dominio.interfaces;
using minimal_api.infraestrutura.Db;
using minimal_api.dominio.Entidades;

namespace minimal_api.dominio.Servicos
{
    public class veiculosServices : iVeiculosServices
    {
        private readonly EstacionamentoContexto _ctx;
        public veiculosServices(EstacionamentoContexto ctx) { _ctx = ctx; }

        public async Task<Veiculo> CriarAsync(Veiculo veiculo, CancellationToken ct = default)
        {
            _ctx.Veiculos.Add(veiculo);
            await _ctx.SaveChangesAsync(ct);
            return veiculo;
        }

        public Task<IReadOnlyList<Veiculo>> ListarAsync(CancellationToken ct = default)
            => _ctx.Veiculos.AsNoTracking().ToListAsync(ct).ContinueWith<IReadOnlyList<Veiculo>>(t => t.Result, ct);
    }
}