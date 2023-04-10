using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Docentes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? Contacto { get; set; }

        public string? Filiacao { get; set; }

        [JsonIgnore]
        [InverseProperty("Docentes")]
        public virtual ICollection <Membros>? Membros { get; set; }

    }
}
