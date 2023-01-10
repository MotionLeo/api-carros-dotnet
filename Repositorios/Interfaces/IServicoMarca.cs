using api.Models;

namespace api.Repositorios.Interfaces;

public interface IServicoMarca
{
    Task<List<Marca>> TodosAsync();
    Task IncluirAsync(Marca marca);
    Task<Marca> AtualizarAsync(Marca marca);
    Task ApagarAsync(Marca marca);
}