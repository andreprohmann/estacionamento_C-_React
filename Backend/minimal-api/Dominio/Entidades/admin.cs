using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace minimal_api.dominio.Entidades;


// Definir a entidade Admin
[Index(nameof(Email), IsUnique = true)]
public class admin
{
    // Definir as propriedades da entidade
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
    [StringLength(50)] public string Nome { get; set; } = default!;
    [Required][EmailAddress] public string Email { get; set; } = default!;
    [Required][StringLength(50)] public string Senha { get; set; } = default!;
    [Required][StringLength(50)] public string Perfil { get; set; } = default!;
}