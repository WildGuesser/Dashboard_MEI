using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.Models
{
    public class Membros
    {
        [Key]
        [Required]
        public int Equipa_Orientadores_Id { get; set; }

        [Required]
        public int Docente_Id { get; set; }

        [ForeignKey("Equipa_Orientadores_Id")]
        [InverseProperty("Membros")]
        public virtual Equipa_Orientadores Equipa_Orientadores { get; set; }

        [ForeignKey("Docente_Id")]
        [InverseProperty("Membros")]
        public virtual Docentes Docentes { get; set; }

    }
}
