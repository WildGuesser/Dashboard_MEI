using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Trabalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do trabalho é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 50 caracteres.")]
        public string Titulo { get; set; }

        public string? Referencia { get; set; }

        [Required(ErrorMessage = "O tipo do trabalho é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "O tipo deve conter apenas letras e números.")]
        public string Tipo { get; set; }

        [Range(0, 20, ErrorMessage = "A nota deve estar entre 0 e 20.")]
        public string? Nota { get; set; }

        [Display(Name = "Observação")]
        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório.")]
        public int Aluno_Id { get; set; }

        [Required(ErrorMessage = "O ID do júri é obrigatório.")]
        public int Juri_Id { get; set; }

        [Required(ErrorMessage = "O ID do orientador é obrigatório.")]
        public int Orientadores_Id { get; set; }

        [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
        [ForeignKey("Empresa")]
        public int Empresa_Id { get; set; }

        [ForeignKey("Aluno_Id")]
        [InverseProperty("Trabalho")]
        public virtual Alunos Alunos { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Juri_Id")]
        [InverseProperty("Trabalho")]
        public virtual Juri? Juri { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Orientadores_Id")]
        [InverseProperty("Trabalho")]
        public virtual Orientadores Orientadores { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Empresa_Id")]
        [InverseProperty("Trabalho")]
        public virtual Empresas? Empresas { get; set; }
    }
}
