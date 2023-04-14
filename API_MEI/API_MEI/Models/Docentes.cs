using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Docentes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A filiação do docente é obrigatória.")]
        [StringLength(100, ErrorMessage = "A filiação do docente não pode ter mais do que 100 caracteres.")]

        [Display(Name = "Filiação")]
        public string Filiacao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Docente_Id")]
        [InverseProperty("Docentes")]
        public virtual Membros Membros { get; set; }
    }
}
