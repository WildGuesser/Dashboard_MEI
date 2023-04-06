using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Trabalho
    {

        public Trabalho() 
        {
            Trabalho_Empresa = new HashSet<Trabalho_Empresa>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string? Referencia { get; set; }

        [Required]
        public string Tipo { get; set; }

        public string? Nota { get; set; }

        public string? Observacao { get; set; }


        public int Alunos_Id { get; set; }


        public int Juri_Id { get; set; }

        public int Equipa_Orientadores_Id { get; set; }

        
        [ForeignKey("Alunos_Id")]
        [InverseProperty("Trabalho")]
        public virtual Alunos Alunos { get; set; }

        
        [ForeignKey("Juri_Id")]
        [InverseProperty("Trabalho")]
        public virtual Juri Juri { get; set; }

        
        [ForeignKey("Equipa_Orientadores_Id")]
        [InverseProperty("Trabalho")]
        public virtual Equipa_Orientadores Equipa_Orientadores { get; set; }


    
        [InverseProperty("Trabalho")]
        public virtual ICollection <Trabalho_Empresa> Trabalho_Empresa { get; set; }

    }
}
