using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_MEI.Models
{
    public class Docentes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // tell EF not to generate a value for this property
        public string Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string? Email { get; set; }

        public string? Contacto { get; set; }

        public string? Filiacao { get; set; }


        [InverseProperty("Docentes")]
        public virtual ICollection <Membros> Membros { get; set; }

    }
}
