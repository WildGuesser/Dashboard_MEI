using API_MEI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.DTOs
{
    public class DocentesDTO
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "O campo 'nome' é obrigatório.")]
        public string Nome { get; set; }

        public string? Email { get; set; }

        public string? Contacto { get; set; }

    }
}
