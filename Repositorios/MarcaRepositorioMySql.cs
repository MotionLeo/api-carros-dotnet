using api.Models;
using api.Repositorios.Interfaces;
using MySql.Data.MySqlClient;

namespace api.ModelViews;

public class MarcaRepositorioMySql : IServicoMarca
{
    public MarcaRepositorioMySql()
    {
        conexao = Environment.GetEnvironmentVariable("DATABASE_URL_LOCACAOCARRO");
        if(conexao is null) conexao = "Server=localhost;Port=3307;Database=locacao_de_carros;Uid=root;Pwd=123456;";
    }
    private string? conexao = null;
    public async Task<List<Marca>> TodosAsync()
    {
        var lista = new List<Marca>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"select * from marcas";

            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                lista.Add(new Marca{
                    Id = Convert.ToInt32(dr["id"]),
                    Nome = dr["nome"].ToString() ?? "",
                });
            }

            conn.Close();
        }

        return lista;
    }

    public async Task IncluirAsync(Marca marca)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into marcas(nome)values(@nome);";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@nome", marca.Nome));
            await command.ExecuteNonQueryAsync();

            // caso queira trabalhar com o ID retornado 
            // int id = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
        }
    }

    public async Task<Marca> AtualizarAsync(Marca marca)
    {
       using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update marcas set nome=@nome where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", marca.Id));
            command.Parameters.Add(new MySqlParameter("@nome", marca.Nome));
            await command.ExecuteNonQueryAsync();

            conn.Close();
        }

        return marca;
    }

    public async Task ApagarAsync(Marca marca)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"delete from marcas where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", marca.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
}