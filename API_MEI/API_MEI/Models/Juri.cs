using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace API_MEI.Models
{
    public class Juri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Data de Defesa é obrigatório.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Insira uma data válida no formato dd/MM/yyyy.")]
        public DateTime Data_Defesa { get; set; }

        [Required(ErrorMessage = "O Id Trabalho é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do júri deve ser maior que 0.")]
        public int Trabalho_Id { get; set; }

        [InverseProperty("Juri")]
        [ForeignKey("Trabalho_Id")]
        public virtual Trabalhos? Trabalho { get; set; }

        [InverseProperty("Juri")]
        public virtual ICollection<JuriMembros> JuriMembros { get; set; }

    }
}
