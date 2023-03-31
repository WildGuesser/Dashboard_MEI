using API_MEI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.DTOs
{
    public class AlunosDTO
    {
        
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)] // tell EF not to generate a value for this property
            public int Id { get; set; }

            [Required]
            public string Nome { get; set; }

            [Required]
            public string Curso { get; set; }

            public string? Filiacao { get; set; }

            public string Email { get; set; }


            [Required]
            public bool Estado { get; set; }

           

    }
}


