using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Trabalho_Empresa
    {
        [Required]
        public int Trabalho_Id { get; set; }

        [Required]
        public int Empresa_Id { get; set; }
        public string? Protocolo { get; set; }

        [JsonIgnore]
        [ForeignKey("Empresa_Id")]
        [InverseProperty("Trabalho_Empresa")]
        public virtual Empresas Empresas { get; set; }

        [JsonIgnore]
        [ForeignKey("Trabalho_Id")]
        [InverseProperty("Trabalho_Empresa")]
        public virtual Trabalho Trabalho { get; set; }
    }
}
