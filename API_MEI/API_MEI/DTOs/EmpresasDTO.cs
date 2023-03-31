using System.ComponentModel.DataAnnotations;

namespace API_MEI.DTOs
{
    public class EmpresasDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Local { get; set; }
    }
}