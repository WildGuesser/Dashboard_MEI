using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API_MEI.Models
{
    public class Especialistas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [EmailAddress]
        public string Email_especialista { get; set; }

        public string? Contacto { get; set; }


        [Required]
        public int Empresa_ID { get; set; }

        [JsonIgnore]
        [InverseProperty("Especialistas")]
        public ICollection <Equipa_Orientadores>? Equipa_Orientadores { get; set; }


        [ForeignKey("Empresa_ID")]
        [InverseProperty("Especialistas")]
        public virtual Empresas? Empresas { get; set; }
    }
}
