using System.Text.Json.Serialization;

namespace api.DTOs;

public record MarcaDTO
{
    [JsonPropertyName("name")]
    public string Nome { get;set; } = default!;
}