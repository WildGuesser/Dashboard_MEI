using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_MEI.Models
{
    public class JuriMembros
    {
        [Required(ErrorMessage = "O campo Juri Id é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Juri Id deve ser maior que 0.")]
        public int Juri_Id { get; set; }

        [Required(ErrorMessage = "O campo Membros Id é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Membros Id deve ser maior que 0.")]
        public int Membro_Id { get; set; }

        [Required(ErrorMessage = "O campo Função é obrigatório.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo Função deve ter entre 2 e 200 caracteres.")]
        [Display(Name = "Função")]
        public string Funcao { get; set; }

        [ForeignKey("Juri_Id")]
        [InverseProperty("JuriMembros")]
        public virtual Juri? Juri { get; set; }

        [ForeignKey("Membro_Id")]
        [InverseProperty("JuriMembros")]
        public virtual Membros? Membros { get; set; }
    }
}
