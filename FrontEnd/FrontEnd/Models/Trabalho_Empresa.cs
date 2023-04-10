using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Trabalho_Empresa
    {
        [Required]
        public int Trabalho_Id { get; set; }

        [Required]
        public int Empresa_Id { get; set; }
        public string? Protocolo { get; set; }


        public virtual Empresas Empresas { get; set; }

        public virtual Trabalho Trabalho { get; set; }
    }
}
