using BlogPessoal.DTOs;
using System.Text;
using System.Text.Json;

namespace BlogPessoal.Services.IA
{
    public class GeminiService : IIAService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GeminiService> _logger;

        // corrigir o issue do JsonSerializerOptions
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public GeminiService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<GeminiService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ResultadoIA?> GerarResumoAsync(string conteudo)
        {
            try
            {
                var apiKey = _configuration["Gemini:ApiKey"]
                    ?? throw new InvalidOperationException("Gemini API Key não configurada!");

                var model = _configuration["Gemini:Model"] ?? "gemini-2.5-flash";
                var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

                var prompt = PromptBuilder.ConstruirPromptResumo(conteudo);

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new[] { new { text = prompt } }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.3,
                        maxOutputTokens = 512
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(requestUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Gemini erro {StatusCode}: {Body}",
                        response.StatusCode, responseBody);
                    return null;
                }

                using var doc = JsonDocument.Parse(responseBody);
                var texto = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                if (string.IsNullOrWhiteSpace(texto))
                    return null;

                texto = texto
                    .Replace("```json", "")
                    .Replace("```", "")
                    .Trim();

                var resultado = JsonSerializer.Deserialize<ResultadoIA>(texto, JsonOptions);

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o Gemini: {Message}", ex.Message);
                return null;
            }
        }
    }
}