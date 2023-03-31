using API_MEI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MEI.DTOs
{
    public class TrabalhoDTO
    {

        [Required]
        public string Titulo { get; set; }

        public string? Referencia { get; set; }

        [Required]
        public string Tipo { get; set; }

        public string? Nota { get; set; }

        public string? Observacao { get; set; }


        public int Alunos_Id { get; set; }


        public int Juri_Id { get; set; }

        public int Equipa_Orientadores_Id { get; set; }


    }
}
 