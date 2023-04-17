using API_MEI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.DTOs
{
    public class Equipa_OrientadoresDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Orientador1 { get; set; }

        public string? Orientador2 { get; set; }

        public string? Orientador3 { get; set; }

    }
}


