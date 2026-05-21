using BlogPessoal.Config;
using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [Authorize]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> GetAll()
        {
            var usuarios = await _service.GetAll();
            return Ok(usuarios.Select(MappingHelper.ToUsuarioResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> GetById(long id)
        {
            var usuario = await _service.GetById(id);

            if (usuario is null)
                return NotFound("Usuário não encontrado!");

            return Ok(MappingHelper.ToUsuarioResponse(usuario));
        }

        [AllowAnonymous]
        [HttpPost("cadastrar")]
        public async Task<ActionResult<UsuarioResponseDTO>> Create(
            [FromBody] UsuarioRequestDTO dto)
        {
            var usuario = MappingHelper.ToUsuario(dto);
            var novoUsuario = await _service.Create(usuario);

            if (novoUsuario is null)
                return BadRequest("E-mail já cadastrado ou dados inválidos!");

            return CreatedAtAction(nameof(GetById),
                new { id = novoUsuario.Id },
                MappingHelper.ToUsuarioResponse(novoUsuario));
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioResponseDTO>> Update(
            [FromBody] UsuarioRequestDTO dto)
        {
            var usuario = MappingHelper.ToUsuario(dto);
            var usuarioAtualizado = await _service.Update(usuario);

            if (usuarioAtualizado is null)
                return NotFound("Usuário não encontrado ou e-mail já cadastrado!");

            return Ok(MappingHelper.ToUsuarioResponse(usuarioAtualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var usuario = await _service.GetById(id);

            if (usuario is null)
                return NotFound("Usuário não encontrado!");

            await _service.Delete(usuario);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioLogin>> Login(
            [FromBody] UsuarioLogin usuarioLogin)
        {
            var resposta = await _service.Autenticar(usuarioLogin);

            if (resposta is null)
                return Unauthorized("E-mail ou senha incorretos!");

            return Ok(resposta);
        }
    }
}