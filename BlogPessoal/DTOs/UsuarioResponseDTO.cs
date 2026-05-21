namespace BlogPessoal.DTOs
{
    public class UsuarioResponseDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string? Foto { get; set; }
    }
}