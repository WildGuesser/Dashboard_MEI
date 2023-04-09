using API_MEI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.DTOs
{
    public class MembrosDTO
    {
        public int Id { get; set; }

        public int Equipa_Orientadores_Id { get; set; }

        public string? Docente_Id { get; set; }

        [ForeignKey("Equipa_Orientadores_Id")]
        [InverseProperty("Membros")]
        public virtual Equipa_OrientadoresDTO Equipa_Orientadores { get; set; }

        [ForeignKey("Docente_Id")]
        [InverseProperty("Membros")]
        public virtual Docentes Docentes { get; set; }
    }
}