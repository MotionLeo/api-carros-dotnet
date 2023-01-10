using api.Models;
using api.Repositorios.Interfaces;

namespace api.ModelViews;

public class ConfiguracaoRepositorio : IServicoConfiguracao
{
    private static List<Configuracao> lista = new List<Configuracao>();

    public async Task<List<Configuracao>> TodosAsync()
    {
        return await Task.FromResult(lista);
    }

    public async Task IncluirAsync(Configuracao configuracao)
    {
        lista.Add(configuracao);
        await Task.FromResult(new {});
    }

    public async Task<Configuracao> AtualizarAsync(Configuracao configuracao)
    {
        if(configuracao.Id == 0) throw new Exception("Id não pode ser zero");

        var configuracaoDb = lista.Find(m => m.Id == configuracao.Id);
        if(configuracaoDb is null)
        {
            throw new Exception("A configuração informado não existe");
        }

        configuracaoDb.DiasDeLocacao = configuracao.DiasDeLocacao;
        return await Task.FromResult(configuracaoDb);
    }

    public async Task ApagarAsync(Configuracao configuracao)
    {
        lista.Remove(configuracao);
        await Task.FromResult(new {});
    }
}