using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs
{
    public class TemaRequestDTO
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "Descrição deve ter no máximo 100 caracteres")]
        public string Descricao { get; set; } = string.Empty; 
    }
}