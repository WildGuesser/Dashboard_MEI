using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_MEI.Models
{
    public class Especialistas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // tell EF not to generate a value for this property
        public string Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string? Email { get; set; }

        public string? Contacto { get; set; }

        public int Empresa_ID { get; set; }


        [InverseProperty("Especialistas")]
        public virtual ICollection <Membros> Membros { get; set; }


        [ForeignKey("Empresa_ID")]
        [InverseProperty("Especialistas")]
        public virtual Empresas? Empresas { get; set; }
    }
}
