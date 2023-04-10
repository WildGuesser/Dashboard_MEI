using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Equipa_Orientadores
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Membros_Id { get; set; }
        public int? Especialista_Id { get; set; }

        [JsonIgnore]
        [InverseProperty("Equipa_Orientadores")]
        public virtual Trabalho Trabalho { get; set; }

        [InverseProperty("Equipa_Orientadores")]
        public virtual ICollection <Membros> Membros { get; set; }

        [ForeignKey("Especialista_Id")]
        [InverseProperty("Equipa_Orientadores")]
        public virtual Especialistas Especialistas { get; set; }
    }
}
