using BlogPessoal.DTOs;
using BlogPessoal.Services.IA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    [Authorize]
    [Route("api/ia")]
    [ApiController]
    public class IAController : ControllerBase
    {
        private readonly IIAService _iaService;

        public IAController(IIAService iaService)
        {
            _iaService = iaService;
        }

        [HttpPost("resumir")]
        public async Task<ActionResult<ResultadoIA>> Resumir([FromBody] string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return BadRequest("Texto não pode ser vazio!");

            var resultado = await _iaService.GerarResumoAsync(texto);

            if (resultado is null)
                return StatusCode(503, new
                {
                    mensagem = "Serviço de IA temporariamente indisponível.",
                    dica = "Verifique os logs do servidor para mais detalhes."
                });

            return Ok(resultado);
        }
    }
}