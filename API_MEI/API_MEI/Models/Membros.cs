using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace API_MEI.Models
{
    public class Membros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo Nome deve ter entre 2 e 200 caracteres.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "O email inserido não é válido.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo Email deve ter entre 2 e 255 caracteres.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(9[1236]|2\d)\d{7}$", ErrorMessage = "Insira um número de telefone português válido.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo Contacto deve ter entre 2 e 255 caracteres.")]
        public string? Contacto { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Membros")]
        public virtual Especialistas? Especialistas { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Membros")]
        public virtual Docentes? Docentes { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Membros")]
        public virtual ICollection<JuriMembros>? JuriMembros { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [InverseProperty("Membros")]
        public virtual ICollection<Orientadores>? Orientadores { get; set; }
    }
}