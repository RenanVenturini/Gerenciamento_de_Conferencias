using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Services;
using Gerenciamento_Conferencias.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_Conferencias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrilhaController : ControllerBase
    {
        private readonly ITrilhaService _trilhaService;

        public TrilhaController(ITrilhaService trilhaService)
        {
            _trilhaService = trilhaService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> CriarTrilhaAsync(TrilhaRequest trilhaRequest)
        {
            await _trilhaService.CriarTrilhaAsync(trilhaRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarTrilhaAsync(AtualizarTrilhaRequest atualizarTrilhaRequest)
        {
            await _trilhaService.AtualizarTrilhaAsync(atualizarTrilhaRequest);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarTrilhaAsync()
        {
            var trilha = await _trilhaService.ListarTrilhaAsync();
            return Ok(trilha);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTrilhaPorId(int id)
        {
            var trilha = await _trilhaService.ObterTrilhaPorId(id);
            return Ok(trilha);
        }

        [HttpDelete("{id}/excluir")]
        public async Task<IActionResult> ExcluirTrilhaAsync(int id)
        {
            await _trilhaService.ExcluirTrilhaAsync(id);
            return NoContent();
        }

    }
}
