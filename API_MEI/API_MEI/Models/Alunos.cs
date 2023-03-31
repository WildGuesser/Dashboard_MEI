using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Alunos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // tell EF not to generate a value for this property
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Curso { get; set; }

        public string Email { get; set; }

        public string Instituição { get; set; } 

        [Required]
        public bool Estado { get; set; }

        [JsonIgnore]
        [InverseProperty("Alunos")]
        public virtual Trabalho Trabalho { get; set; }

    }
}
