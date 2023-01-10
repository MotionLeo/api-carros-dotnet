using api.DTOs;
using api.Models;
using api.Repositorios.Interfaces;
using api.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("configuracoes")]
    public class ConfiguracoesController : ControllerBase
    {
        private IServicoConfiguracao _servico;
        public ConfiguracoesController(IServicoConfiguracao servico)
        {
            _servico = servico;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var configuracoes = await _servico.TodosAsync();
            return StatusCode(200, configuracoes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromBody] int id)
        {
            var configuracao = (await _servico.TodosAsync()).Find(m => m.Id == id);
            return StatusCode(200, configuracao);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create ([FromBody] ConfiguracaoDTO configuracaoDTO)
        {
            var configuracao = BuilderServico<Configuracao>.Builder(configuracaoDTO);
            await _servico.IncluirAsync(configuracao);
            return StatusCode(201, configuracao);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Configuracao configuracao)
        {
            if (id != configuracao.Id)
            {
                return StatusCode(400, new {
                    Mensagem = "O id da configuração precisa bater com o id da url"
                });
            }
            var configuracaoDB = await _servico.AtualizarAsync(configuracao);
            return StatusCode(200, configuracaoDB);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete ([FromRoute] int id)
        {
            var configuracaoDb = (await _servico.TodosAsync()).Find(c => c.Id == id);
            if(configuracaoDb is null)
            {
                return StatusCode(404, new {
                    Mensagem = "A configuração informada não existe"
                });
            }

            await _servico.ApagarAsync(configuracaoDb);

            return StatusCode(204);
        }
    }
}