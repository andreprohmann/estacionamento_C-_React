namespace minimal_api.dominio.DTOs;

public record veiculoDTO
{
    // Definir as propriedades da entidade    
    public string Nome { get; set; } = default!;
    public string Placa { get; set; } = default!;
    public string Marca { get; set; } = default!;
    public int Ano { get; set; } = default!;
    public DateTime checkIn { get; set; } = default!;
    public DateTime checkOut { get; set; } = default!;
    public double valorTotal { get; set; } = default!;
}
