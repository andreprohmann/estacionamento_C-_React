using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minimal_api.dominio.Entidades;

// Definir a entidade Veiculo
public class veiculo
{
    // Definir as propriedades da entidade
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
    [StringLength(50)] public string Nome { get; set; } = default!;
    [Required][EmailAddress] public string Placa { get; set; } = default!;
    [StringLength(50)] public string Marca { get; set; } = default!;
    [StringLength(50)] public int Ano { get; set; } = default!;
}