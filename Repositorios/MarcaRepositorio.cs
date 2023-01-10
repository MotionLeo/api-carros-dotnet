using api.Models;
using api.Repositorios.Interfaces;

namespace api.ModelViews;

public class MarcaRepositorio : IServicoMarca
{
    private static List<Marca> lista = new List<Marca>();

    public async Task<List<Marca>> TodosAsync()
    {
        return await Task.FromResult(lista);
    }

    public async Task IncluirAsync(Marca marca)
    {
        lista.Add(marca);
        await Task.FromResult(new {});
    }

    public async Task<Marca> AtualizarAsync(Marca marca)
    {
        if(marca.Id == 0) throw new Exception("Id não pode ser zero");

        var marcaDb = lista.Find(m => m.Id == marca.Id);
        if(marcaDb is null)
        {
            throw new Exception("A marca informado não existe");
        }

        marcaDb.Nome = marca.Nome;
        return await Task.FromResult(marcaDb);
    }

    public async Task ApagarAsync(Marca marca)
    {
        lista.Remove(marca);
        await Task.FromResult(new {});
    }
}