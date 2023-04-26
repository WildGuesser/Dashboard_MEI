﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
    public class Trabalhos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do trabalho é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 255 caracteres.")]
        public string Titulo { get; set; }

        [StringLength(255, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 255 caracteres.")]
        public string? ReferenciaInfo { get; set; }

        [Required(ErrorMessage = "O tipo do trabalho é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "O tipo deve conter apenas letras e números.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres.")]
        public string Tipo { get; set; }

        [Range(0, 20, ErrorMessage = "A nota deve estar entre 0 e 20.")]
        public string? Nota { get; set; }

        [Display(Name = "Adenda ou Protocolo")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 255 caracteres.")]
        public string? AdendaProtocolo { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do aluno deve ser maior que 0.")]
        public int Aluno_Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O ID do júri deve ser maior que 0.")]
        public int? Juri_Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O ID da empresa deve ser maior que 0.")]
        public int? Empresa_Id { get; set; }

        [ForeignKey("Aluno_Id")]
        [InverseProperty("Trabalho")]
        public virtual Alunos Alunos { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Juri_Id")]
        [InverseProperty("Trabalho")]
        public virtual Juri? Juri { get; set; }

        [InverseProperty("Trabalho")]
        public virtual ICollection<Orientadores> Orientadores { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Empresa_Id")]
        [InverseProperty("Trabalho")]
        public virtual Empresas? Empresas { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Orientadores? Orientador { get; set; }
    }
}
