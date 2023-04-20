using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Alunos
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O número do aluno é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número do aluno deve ser maior que zero.")]
        [Display(Name = "Numero Aluno")]
        public int Numero_Aluno { get; set; }

        [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome do aluno deve ter no máximo 200 caracteres.")]
        [Display(Name = "Nome Aluno")]
        public string Nome { get; set; }

        [StringLength(200, ErrorMessage = "O curso do aluno deve ter no máximo 200 caracteres.")]
        public string? Curso { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [StringLength(200, ErrorMessage = "O curso do aluno deve ter no máximo 200 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        [Display(Name = "Email Aluno")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Contacto é obrigatório.")]
        [RegularExpression(@"^9[1236]\d{7}$", ErrorMessage = "O campo Contacto deve ser um número de telefone móvel válido em Portugal.")]
        [StringLength(200, ErrorMessage = "O Contacto deve ter no máximo 200 caracteres.")]
        [Display(Name = "Contacto Aluno")]
        public string Contacto { get; set; }

        [StringLength(200, ErrorMessage = "O nome da instituição deve ter no máximo 200 caracteres.")]
        [Display(Name = "Instituição Aluno")]
        public string? Instituicao { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        public bool Estado { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Alunos")]
        public virtual Trabalhos? Trabalho { get; set; } = null;
    }
}
