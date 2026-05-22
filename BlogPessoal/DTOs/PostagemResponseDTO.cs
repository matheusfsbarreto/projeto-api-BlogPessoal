namespace BlogPessoal.DTOs
{
    public class PostagemResponseDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public TemaResponseDTO? Tema { get; set; }
        public UsuarioResponseDTO? Usuario { get; set; }

        public string? ResumoIA { get; set; }
        public string? TagsIA { get; set; }
        public string? CategoriaIA { get; set; }
    }
}