using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Orientadores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [InverseProperty("Orientadores")]
        public virtual ICollection<Trabalho> Trabalho { get; set; }

        [InverseProperty("Orientadores")]
        public virtual ICollection<OrientadoresMembros> OrientadoresMembros { get; set; }

    }
}
