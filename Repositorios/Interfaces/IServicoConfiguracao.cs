using api.Models;

namespace api.Repositorios.Interfaces;

public interface IServicoConfiguracao
{
    Task<List<Configuracao>> TodosAsync();
    Task IncluirAsync(Configuracao configuracao);
    Task<Configuracao> AtualizarAsync(Configuracao configuracao);
    Task ApagarAsync(Configuracao configuracao);
}