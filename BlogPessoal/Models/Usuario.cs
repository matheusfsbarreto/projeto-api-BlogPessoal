using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Usuario1 { get; set; } = string.Empty; 

        [JsonIgnore]
        [Required]
        [StringLength(255)]
        public string Senha { get; set; } = string.Empty;

        [StringLength(5000)]
        public string? Foto { get; set; }

        [JsonIgnore]
        public ICollection<Postagem>? Postagens { get; set; }
    }
}