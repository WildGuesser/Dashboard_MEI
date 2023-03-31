using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.Models
{
    public class Empresas
    {
        public Empresas()
        {
            Especialistas = new HashSet<Especialistas> ();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Local { get; set; }


        [InverseProperty("Empresas")]
        public virtual ICollection <Especialistas>? Especialistas { get; set; }


        [InverseProperty("Empresas")]
        public virtual ICollection <Trabalho_Empresa> Trabalho_Empresa{ get; set; }
    }
}
