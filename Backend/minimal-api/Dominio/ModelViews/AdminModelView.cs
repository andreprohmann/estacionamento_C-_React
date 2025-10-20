using minimal_api.Dominio.Enuns;

namespace minimal_api.ModelViews;

public record AdminModelView
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Perfil { get; set; } = default!;
}