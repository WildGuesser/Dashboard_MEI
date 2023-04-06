using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Equipa_Orientadores
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Orientador1 { get; set; }

        public string? Orientador2 { get; set; }

        public string? Orientador3 { get; set; }

        [JsonIgnore]
        [InverseProperty("Equipa_Orientadores")]
        public virtual Trabalho Trabalho { get; set; }

        [InverseProperty("Equipa_Orientadores")]
        public virtual ICollection <Membros> Membros { get; set; }


    }
}
