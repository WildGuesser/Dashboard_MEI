using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.DTOs.Tabela_Docente
{
    public class DocentesDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A filiação do docente é obrigatória.")]
        [StringLength(100, ErrorMessage = "A filiação do docente não pode ter mais do que 100 caracteres.")]

        [Display(Name = "Filiação")]
        public string Filiacao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Id")]
        [InverseProperty("Docentes")]
        public virtual MembrosDTO_D Membros { get; set; }
    }
}
