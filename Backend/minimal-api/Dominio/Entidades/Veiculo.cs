using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace minimal_api.dominio.Entidades;

// Definir a entidade Veiculo
public class veiculo
{
    // Definir as propriedades da entidade
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
    [StringLength(50)] public string Nome { get; set; } = default!;
    [StringLength(50)] public string Placa { get; set; } = default!;
    [StringLength(50)] public string Marca { get; set; } = default!;
    [StringLength(50)][Range(1900, 2026)] public int Ano { get; set; } = default!;
    [Required] public DateTime checkIn { get; set; } = default!;
    [Required] public DateTime checkOut { get; set; } = default!;
    [StringLength(50)] public double valorTotal { get; set; } = default!;
}
