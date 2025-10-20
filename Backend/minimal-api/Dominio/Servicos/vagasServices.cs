using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using minimal_api.dominio.interfaces;
using minimal_api.infraestrutura.Db;
using minimal_api.dominio.Entidades;

namespace minimal_api.dominio.Servicos
{
    public class VagasServices : iVagasServices
    {
        private readonly EstacionamentoContexto _ctx;

        public VagasServices(EstacionamentoContexto ctx)
        {
            _ctx = ctx;
        }

        public async Task OcuparAsync(long vagaId, long veiculoId, CancellationToken ct = default)
        {
            using var tx = await _ctx.Database.BeginTransactionAsync(ct);

            var vaga = await _ctx.Vagas.AsTracking()
                .FirstOrDefaultAsync(v => v.Id == vagaId, ct)
                ?? throw new InvalidOperationException("Vaga não encontrada.");

            if (!vaga.Ativa)
                throw new InvalidOperationException("Vaga inativa.");

            var vagaOcupada = await _ctx.Ocupacoes
                .AnyAsync(o => o.VagaId == vagaId && o.CheckOut == null, ct);
            if (vagaOcupada)
                throw new InvalidOperationException("Vaga já ocupada.");

            var veiculoExiste = await _ctx.Veiculos.AnyAsync(v => v.Id == veiculoId, ct);
            if (!veiculoExiste)
                throw new InvalidOperationException("Veículo não encontrado.");

            var veiculoOcupado = await _ctx.Ocupacoes
                .AnyAsync(o => o.VeiculoId == veiculoId && o.CheckOut == null, ct);
            if (veiculoOcupado)
                throw new InvalidOperationException("Veículo já está estacionado.");

            _ctx.Ocupacoes.Add(new Ocupacao
            {
                VagaId = vagaId,
                VeiculoId = veiculoId,
                CheckIn = DateTime.UtcNow
            });

            await _ctx.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }

        public async Task LiberarAsync(long vagaId, CancellationToken ct = default)
        {
            using var tx = await _ctx.Database.BeginTransactionAsync(ct);

            var ocupacao = await _ctx.Ocupacoes
                .FirstOrDefaultAsync(o => o.VagaId == vagaId && o.CheckOut == null, ct);

            if (ocupacao == null)
                throw new InvalidOperationException("A vaga já está livre.");

            ocupacao.CheckOut = DateTime.UtcNow;

            await _ctx.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }

        public async Task<List<(string NumeroVaga, string? Placa)>> OcupacaoAtualAsync(CancellationToken ct = default)
        {
            var query =
                from v in _ctx.Vagas
                join o in _ctx.Ocupacoes.Where(x => x.CheckOut == null)
                    on v.Id equals o.VagaId into grp
                from oaberta in grp.DefaultIfEmpty()
                select new
                {
                    NumeroVaga = v.Numero,
                    Placa = oaberta != null ? oaberta.Veiculo.Placa : null
                };

            var dados = await query.ToListAsync(ct);
            return dados.Select(x => (x.NumeroVaga, x.Placa)).ToList();
        }
    }
}