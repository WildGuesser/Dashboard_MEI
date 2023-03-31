using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.Models
{
    public class Membros
    {

        [Key]
        public int Id { get; set; }

        public int Equipa_Orientadores_Id { get; set; }

        public string? Docente_Id { get; set; }

        public string? Especialista_Id { get; set; }

        [ForeignKey("Equipa_Orientadores_Id")]
        [InverseProperty("Membros")]
        public virtual Equipa_Orientadores Equipa_Orientadores { get; set; }

        [ForeignKey("Docente_Id")]
        [InverseProperty("Membros")]
        public virtual Docentes Docentes { get; set; }


        [ForeignKey("Especialista_Id")]
        [InverseProperty("Membros")]
        public virtual Especialistas Especialistas { get; set; }

    }
}
