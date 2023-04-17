using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class OrientadoresMembros
    {


        [Display(Name = "ID do Orientador")]
        public int OrientadorId { get; set; }


        [Display(Name = "ID do Membro")]
        public int MembrosId { get; set; }

        [Required(ErrorMessage = "O campo Função é obrigatório.")]
        [Display(Name = "Função")]
        [StringLength(50, ErrorMessage = "O campo Função deve ter no máximo 50 caracteres.")]
        public string Funcao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("OrientadorId")]
        [InverseProperty("OrientadoresMembros")]
        public virtual Orientadores Orientadores { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("MembrosId")]
        [InverseProperty("OrientadoresMembros")]
        public virtual Membros Membros { get; set; }
    }
}