using System.Threading;
using System.Threading.Tasks;

namespace minimal_api.dominio.interfaces
{
    public interface iAdminServices
    {
        Task<bool> ValidarLoginAsync(string email, string senha, CancellationToken ct = default);
    }
}