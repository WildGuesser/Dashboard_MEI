using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace   FrontEnd.Models
{
    public class Especialistas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
        public int Empresa_ID { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Id")]
        [InverseProperty("Especialistas")]
        public virtual Membros Membros { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("Empresa_ID")]
        [InverseProperty("Especialistas")]
        public virtual Empresas? Empresas { get; set; }
    }

}
