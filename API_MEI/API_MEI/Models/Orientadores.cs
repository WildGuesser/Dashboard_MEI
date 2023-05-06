using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Orientadores
    {
        [Display(Name = "ID do Trabalho")]
        [Required(ErrorMessage = "O ID do Trabalho é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do Trabalho deve ser maior que 0.")]
        public int Trabalho_Id { get; set; }

        [Display(Name = "ID do Membro")]
        [Required(ErrorMessage = "O ID do Membro é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do Membro deve ser maior que 0.")]
        public int Membro_Id { get; set; }

        [Required(ErrorMessage = "O campo Função é obrigatório.")]
        [Display(Name = "Função")]
        [StringLength(100, ErrorMessage = "O campo Função deve ter no máximo 100 caracteres.")]
        public string Funcao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Trabalho_Id")]
        [InverseProperty("Orientadores")]
        public virtual Trabalhos? Trabalho { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Membro_Id")]
        [InverseProperty("Orientadores")]
        public virtual Membros? Membros { get; set; }
    }
}
