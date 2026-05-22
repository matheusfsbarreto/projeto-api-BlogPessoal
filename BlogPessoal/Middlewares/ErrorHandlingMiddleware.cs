using BlogPessoal.DTOs;
using System.Net;
using System.Text.Json;

namespace BlogPessoal.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // se der qualquer erro inesperado, cai aqui
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                await TratarExcecao(context, ex);
            }
        }

        private static async Task TratarExcecao(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var resposta = new ErrorResponseDTO();

            switch (ex)
            {
                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    resposta.Status = 404;
                    resposta.Mensagem = "Recurso não encontrado";
                    resposta.Detalhe = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    resposta.Status = 401;
                    resposta.Mensagem = "Acesso não autorizado";
                    resposta.Detalhe = ex.Message;
                    break;

                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    resposta.Status = 400;
                    resposta.Mensagem = "Dados inválidos";
                    resposta.Detalhe = ex.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    resposta.Status = 500;
                    resposta.Mensagem = "Erro interno do servidor";
                    resposta.Detalhe = ex.Message;
                    break;
            }

            var json = JsonSerializer.Serialize(resposta);
            await context.Response.WriteAsync(json);
        }
    }
}