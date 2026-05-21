using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Models
{
    public class Postagem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Texto { get; set; } = string.Empty;

        public DateTime Data { get; set; } = DateTime.Now;

        [ForeignKey("Tema")]
        public long? TemaId { get; set; }
        public Tema? Tema { get; set; }

        [ForeignKey("Usuario")]
        public long? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [StringLength(500)]
        public string? ResumoIA { get; set; }

        [StringLength(200)]
        public string? TagsIA { get; set; }

        [StringLength(100)]
        public string? CategoriaIA { get; set; }
    }
}