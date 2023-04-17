using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace API_MEI.DTOs.Tabela_Docente
{
    public class MembrosDTO_D
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo Nome deve ter entre 2 e 100 caracteres.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "O email inserido não é válido.")]
        public string Email { get; set; }

        [RegularExpression(@"^(9[1236]|2\d)\d{7}$", ErrorMessage = "Insira um número de telefone português válido.")]
        public string? Contacto { get; set; }


    }
}