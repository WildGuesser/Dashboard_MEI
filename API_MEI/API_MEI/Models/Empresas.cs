using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Empresas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da empresa deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O local da empresa é obrigatório.")]
        [StringLength(100, ErrorMessage = "O local da empresa deve ter no máximo 100 caracteres.")]
        public string Local { get; set; }

        [EmailAddress(ErrorMessage = "O email da empresa deve ser um endereço de email válido.")]
        public string Email_empresa { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Empresas")]
        public virtual ICollection<Especialistas>? Especialistas { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Empresas")]
        public virtual ICollection<Trabalho>? Trabalho { get; set; }
    }
}
