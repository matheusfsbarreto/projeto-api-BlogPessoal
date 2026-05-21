using BlogPessoal.Config;
using BlogPessoal.DTOs;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [Authorize]
    [Route("api/temas")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _service;

        public TemaController(ITemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemaResponseDTO>>> GetAll()
        {
            var temas = await _service.GetAll();
            return Ok(temas.Select(MappingHelper.ToTemaResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemaResponseDTO>> GetById(long id)
        {
            var tema = await _service.GetById(id);

            if (tema is null)
                return NotFound("Tema não encontrado!");

            return Ok(MappingHelper.ToTemaResponse(tema));
        }

        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult<IEnumerable<TemaResponseDTO>>> GetByDescricao(
            string descricao)
        {
            var temas = await _service.GetByDescricao(descricao);
            return Ok(temas.Select(MappingHelper.ToTemaResponse));
        }

        [HttpPost]
        public async Task<ActionResult<TemaResponseDTO>> Create(
            [FromBody] TemaRequestDTO dto)
        {
            var tema = MappingHelper.ToTema(dto);
            var novoTema = await _service.Create(tema);

            if (novoTema is null)
                return BadRequest("Dados inválidos!");

            return CreatedAtAction(nameof(GetById),
                new { id = novoTema.Id },
                MappingHelper.ToTemaResponse(novoTema));
        }

        [HttpPut]
        public async Task<ActionResult<TemaResponseDTO>> Update(
            [FromBody] TemaRequestDTO dto)
        {
            var tema = MappingHelper.ToTema(dto);
            var temaAtualizado = await _service.Update(tema);

            if (temaAtualizado is null)
                return NotFound("Tema não encontrado!");

            return Ok(MappingHelper.ToTemaResponse(temaAtualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var tema = await _service.GetById(id);

            if (tema is null)
                return NotFound("Tema não encontrado!");

            await _service.Delete(tema);
            return NoContent();
        }
    }
}