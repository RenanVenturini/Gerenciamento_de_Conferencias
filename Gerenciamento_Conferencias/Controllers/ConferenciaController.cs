using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_Conferencias.Data;
using Gerenciamento_Conferencias.Data.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gerenciamento_Conferencias.Services.Interfaces;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenciaController : ControllerBase
    {
        private readonly IConferenciasService _conferenciasService;

        public ConferenciaController(IConferenciasService conferenciasService)
        {
            _conferenciasService = conferenciasService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> CriarConferenciaAsync(ConferenciaRequest conferenciaRequest)
        {
            await _conferenciasService.CriarConferenciaAsync(conferenciaRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarConferenciaAsync(AtualizarConferenciaRequest conferenciaRequest)
        {
            await _conferenciasService.AtualizarConferenciaAsync(conferenciaRequest);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarConferenciaAsync()
        {
            var conferencia = await _conferenciasService.ListarConferenciaAsync();
            return Ok(conferencia);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterConferenciaPorIdAsync(int id)
        {
            var conferencia = await _conferenciasService.ObterConferenciaPorIdAsync(id);
            return Ok(conferencia);
        }

        [HttpDelete("{id}/excluir")]
        public async Task<IActionResult> ExcluirConferenciaAsync(int id)
        {
            await _conferenciasService.ExcluirConferenciaAsync(id);
            return NoContent();
        }
    }
}
