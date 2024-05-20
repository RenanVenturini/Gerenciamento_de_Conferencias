using Gerenciamento_Conferencias.Data.Table;
using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Services;
using Gerenciamento_Conferencias.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_Conferencias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestraController : Controller
    {
        private readonly IPalestraService _palestraService;

        public PalestraController(IPalestraService palestraService)
        {
            _palestraService = palestraService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> CriarPalestraAsync(PalestraRequest palestraRequest)
        {
            await _palestraService.CriarPalestraAsync(palestraRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarPalestraAsync(AtualizarPalestraRequest palestraRequest)
        {
            await _palestraService.AtualizarPalestraAsync(palestraRequest);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarPalestraAsync(int trilhaId)
        {
            var palestra = await _palestraService.ListarPalestraAsync(trilhaId);
            return Ok(palestra);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPalestraPorIdAsync(int id)
        {
            var palestra = await _palestraService.ObterPalestraPorIdAsync(id);
            return Ok(palestra);
        }

        [HttpDelete("{id}/excluir")]
        public async Task<IActionResult> ExcluirPalestraAsync(int id)
        {
            await _palestraService.ExcluirPalestraAsync(id);
            return NoContent();
        }
    }
}
