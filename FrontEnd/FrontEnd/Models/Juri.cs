using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Juri
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Presidente { get; set; }

        public string? Vogal_Arguente1 { get; set; }

        public string? Vogal_Arguente2 { get; set; }

        [Required]
        public string Vogal_Orientador { get; set; }

        [Required]
        public DateTime Data_Defesa { get; set; }

        [JsonIgnore]
        [InverseProperty("Juri")]
        public virtual Trabalho Trabalho { get; set; }  

    }
}
