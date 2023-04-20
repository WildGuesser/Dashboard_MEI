using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Empresas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da empresa deve ter no máximo 200 caracteres.")]
        [Display(Name = "Nome Empresa")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O local da empresa é obrigatório.")]
        [StringLength(255, ErrorMessage = "O local da empresa deve ter no máximo 255 caracteres.")]
        public string Local { get; set; }

        [StringLength(255, ErrorMessage = "O local da empresa deve ter no máximo 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O email da empresa deve ser um endereço de email válido.")]
        [Display(Name = "Email Empresa")]
        public string? Email_empresa { get; set; }

        [StringLength(255, ErrorMessage = "O Protocolo deve ter no máximo 255 caracteres.")]
        public string? Protocolo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Empresas")]
        public virtual ICollection<Especialistas>? Especialistas { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Empresas")]
        public virtual ICollection<Trabalhos>? Trabalho { get; set; }
    }
}

