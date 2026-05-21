using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs
{
    public class PostagemRequestDTO
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(100, ErrorMessage = "Título deve ter no máximo 100 caracteres")]
        public string Titulo { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Texto é obrigatório")]
        [StringLength(1000, ErrorMessage = "Texto deve ter no máximo 1000 caracteres")]
        public string Texto { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Tema é obrigatório")]
        public long? TemaId { get; set; }

        [Required(ErrorMessage = "Usuário é obrigatório")]
        public long? UsuarioId { get; set; }
    }
}