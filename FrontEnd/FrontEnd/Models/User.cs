using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FrontEnd.Models
{
    public class User
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        [Required(ErrorMessage = "A Username é obrigatório.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "O Username deve ter entre 3 e 15 caracteres.")]
        [Display(Name = "Username")]
        public virtual string Username { get; set; }
    }
}
