using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs
{
    public class UsuarioRequestDTO
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(150)]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 255 caracteres")]
        public string Senha { get; set; } = string.Empty;

        public string? Foto { get; set; }
    }
}