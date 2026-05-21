namespace BlogPessoal.Services.IA
{
    public static class PromptBuilder
    {
        public static string ConstruirPromptResumo(string conteudo)
        {
            // retornar apenas JSON
            return $@"
Analise o seguinte texto de uma postagem de blog e retorne APENAS um JSON válido,
sem explicações, sem markdown, sem blocos de código.

Texto da postagem:
{conteudo}

Retorne exatamente neste formato:
{{
  ""resumo"": ""resumo curto em até 2 frases"",
  ""tags"": ""tag1, tag2, tag3"",
  ""categoria"": ""uma categoria sugerida""
}}";
        }
    }
}