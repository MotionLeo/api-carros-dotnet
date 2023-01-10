using System.Text.Json.Serialization;

namespace api.DTOs;

public record ConfiguracaoDTO
{
    [JsonPropertyName("leaseDate")]
    public DateTime DiasDeLocacao { get;set; } = default!;
}