namespace BlogPessoal.DTOs
{
    public class ErrorResponseDTO
    {
        public int Status { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string? Detalhe { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}