using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Trabalho_Empresa
    {

        [Key]
        public int Id { get; set; }
        public string Protocolo { get; set; }   

        public int Empresa_Id { get; set; }
        public int Trabalho_Id { get; set; }

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
