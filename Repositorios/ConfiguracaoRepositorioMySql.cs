using api.Models;
using api.Repositorios.Interfaces;
using MySql.Data.MySqlClient;

namespace api.ModelViews;

public class ConfiguracaoRepositorioMySql : IServicoConfiguracao
{
    public ConfiguracaoRepositorioMySql()
    {
        conexao = Environment.GetEnvironmentVariable("DATABASE_URL_LOCACAOCARRO");
        if(conexao is null) conexao = "Server=localhost;Port=3307;Database=locacao_de_carros;Uid=root;Pwd=123456;";
    }
    private string? conexao = null;
    public async Task<List<Configuracao>> TodosAsync()
    {
        var lista = new List<Configuracao>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"select * from configuracoes";

            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                lista.Add(new Configuracao{
                    Id = Convert.ToInt32(dr["id"]),
                    DiasDeLocacao = Convert.ToDateTime(dr["dias_de_locacao"] ?? ""),
                });
            }

            conn.Close();
        }

        return lista;
    }

    public async Task IncluirAsync(Configuracao configuracao)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into configuracoes(dias_de_locacao)values(@dias);";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@dias", configuracao.DiasDeLocacao));
            await command.ExecuteNonQueryAsync();

            // caso queira trabalhar com o ID retornado 
            // int id = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
        }
    }

    public async Task<Configuracao> AtualizarAsync(Configuracao configuracao)
    {
       using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update configuracoes set dias_de_locacao=@data where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", configuracao.Id));
            command.Parameters.Add(new MySqlParameter("@data", configuracao.DiasDeLocacao));
            await command.ExecuteNonQueryAsync();

            conn.Close();
        }

        return configuracao;
    }

    public async Task ApagarAsync(Configuracao configuracao)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"delete from configuracoes where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", configuracao.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
}