namespace api.Models;

public record Configuracao
{
    public int Id { get;set; } = default!;
    public DateTime DiasDeLocacao { get;set; } = default!;
}