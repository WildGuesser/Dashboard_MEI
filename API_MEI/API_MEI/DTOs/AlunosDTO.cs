using API_MEI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.DTOs
{
    public class AlunosDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O número do aluno deve ser maior que zero.")]
        public int Numero_Aluno { get; set; }

        [Required]

        [StringLength(100)]
        public string Nome { get; set; }

        [Required]

        [StringLength(100)]
        public string Curso { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        [StringLength(100)]
        public string Instituição { get; set; }

        [Required]
        public bool Estado { get; set; }

        [JsonIgnore]
        [InverseProperty("Alunos")]
        public virtual Trabalho? Trabalho { get; set; }
    }
}


