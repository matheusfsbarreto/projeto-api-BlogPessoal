using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models
{
    public class UsuarioLogin
    {
        public long? Id { get; set; }

        public string Nome { get; set; } = string.Empty; 

        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        public string Foto { get; set; } = string.Empty; 

        public string Token { get; set; } = string.Empty; 
    }
}