using BlogPessoal.Config;
using BlogPessoal.DTOs;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [Authorize]
    [Route("api/postagens")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _service;

        public PostagemController(IPostagemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> GetAll()
        {
            var postagens = await _service.GetAll();
            return Ok(postagens.Select(MappingHelper.ToPostagemResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostagemResponseDTO>> GetById(long id)
        {
            var postagem = await _service.GetById(id);

            if (postagem is null)
                return NotFound("Postagem não encontrada!");

            return Ok(MappingHelper.ToPostagemResponse(postagem));
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> GetByTitulo(string titulo)
        {
            var postagens = await _service.GetByTitulo(titulo);
            return Ok(postagens.Select(MappingHelper.ToPostagemResponse));
        }

        [HttpPost]
        public async Task<ActionResult<PostagemResponseDTO>> Create(
            [FromBody] PostagemRequestDTO dto)
        {
            var postagem = MappingHelper.ToPostagem(dto);
            var novaPostagem = await _service.Create(postagem);

            if (novaPostagem is null)
                return BadRequest("Tema não encontrado ou dados inválidos!");

            return CreatedAtAction(nameof(GetById),
                new { id = novaPostagem.Id },
                MappingHelper.ToPostagemResponse(novaPostagem));
        }

        [HttpPut]
        public async Task<ActionResult<PostagemResponseDTO>> Update(
            [FromBody] PostagemRequestDTO dto)
        {
            var postagem = MappingHelper.ToPostagem(dto);
            var postagemAtualizada = await _service.Update(postagem);

            if (postagemAtualizada is null)
                return NotFound("Postagem ou Tema não encontrado!");

            return Ok(MappingHelper.ToPostagemResponse(postagemAtualizada));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var postagem = await _service.GetById(id);

            if (postagem is null)
                return NotFound("Postagem não encontrada!");

            await _service.Delete(postagem);
            return NoContent();
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<IEnumerable<PostagemResponseDTO>>> GetByFiltro(
            [FromQuery] long? autor,
            [FromQuery] long? tema)
        {
            if (autor is null && tema is null)
                return BadRequest("Informe pelo menos um filtro: autor ou tema.");

            var postagens = await _service.GetByFiltro(autor, tema);
            return Ok(postagens.Select(MappingHelper.ToPostagemResponse));
        }
    }
}