using minimal_api.Dominio.Enuns;

namespace minimal_api.dominio.DTO;

public class AdminDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public Perfil? Perfil { get; set; } = default!;

}