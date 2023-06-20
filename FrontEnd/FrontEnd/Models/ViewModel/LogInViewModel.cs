using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FrontEnd.Models.ViewModel
{
    public class LogInViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        [Required(ErrorMessage = "A Username é obrigatório.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "O Username deve ter entre 3 e 15 caracteres.")]
        [Display(Name = "Username")]
        public virtual string Username { get; set; }    


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Required(ErrorMessage = "A Password é obrigatório.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "A Password deve ter entre 5 e 20 caracteres.")]
        [Display(Name = "Password")]
        public virtual string Password { get; set; }
    }
}
