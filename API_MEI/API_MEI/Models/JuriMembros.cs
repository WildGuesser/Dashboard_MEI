using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_MEI.Models
{
    public class JuriMembros
    {
        [Required]
        public int Juri_Id { get; set; }

        [Required]
        public int Membros_Id { get; set; }

        [Required(ErrorMessage = "O campo Função é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo Função deve ter entre 2 e 50 caracteres.")]
        [Display(Name = "Função")]
        public string Funcao { get; set; }

        [ForeignKey("Juri_Id")]
        [InverseProperty("JuriMembros")]
        public virtual Juri Juri { get; set; }

        [ForeignKey("Membros_Id")]
        [InverseProperty("JuriMembros")]
        public virtual Membros Membros { get; set; }
    }
}
