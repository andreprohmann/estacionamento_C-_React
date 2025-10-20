using System.Threading;
using System.Threading.Tasks;
using minimal_api.dominio.interfaces;
using minimal_api.infraestrutura.Db;

namespace minimal_api.dominio.Servicos
{
    public class adminServices : iAdminServices
    {
        private readonly EstacionamentoContexto _ctx;
        public adminServices(EstacionamentoContexto ctx) { _ctx = ctx; }

        public Task<bool> ValidarLoginAsync(string email, string senha, CancellationToken ct = default)
        {
            // Implementação mínima/temporária: sempre verdadeiro.
            // Ajuste conforme sua entidade Admin e regra real.
            return Task.FromResult(true);
        }
    }
}