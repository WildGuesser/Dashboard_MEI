using API_MEI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.DTOs
{
    public class EspecialistasDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string? Email { get; set; }

        public string? Contacto { get; set; }

        [Required]
        public int Empresa_ID { get; set; }

        [JsonIgnore]
        [InverseProperty("Especialistas")]
        public ICollection<Equipa_OrientadoresDTO> Equipa_Orientadores { get; set; }


        [ForeignKey("Empresa_ID")]
        [InverseProperty("Especialistas")]
        public virtual Empresas? Empresas { get; set; }
    }
}
