using api.DTOs;
using api.Models;
using api.Repositorios.Interfaces;
using api.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("marcas")]
    public class MarcasController : ControllerBase
    {
        private IServicoMarca _servico;
        public MarcasController(IServicoMarca servico)
        {
            _servico = servico;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var marcas = await _servico.TodosAsync();
            return StatusCode(200, marcas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromBody] int id)
        {
            var marca = (await _servico.TodosAsync()).Find(m => m.Id == id);
            return StatusCode(200, marca);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create ([FromBody] MarcaDTO marcaDTO)
        {
            var marca = BuilderServico<Marca>.Builder(marcaDTO);
            await _servico.IncluirAsync(marca);
            return StatusCode(201, marca);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Marca marca)
        {
            if (id != marca.Id)
            {
                return StatusCode(400, new {
                    Mensagem = "O id da marca precisa bater com o id da url"
                });
            }
            var marcaDB = await _servico.AtualizarAsync(marca);
            return StatusCode(200, marcaDB);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete ([FromRoute] int id)
        {
            var marcaDb = (await _servico.TodosAsync()).Find(c => c.Id == id);
            if(marcaDb is null)
            {
                return StatusCode(404, new {
                    Mensagem = "A marca informada não existe"
                });
            }

            await _servico.ApagarAsync(marcaDb);

            return StatusCode(204);
        }
    }
}