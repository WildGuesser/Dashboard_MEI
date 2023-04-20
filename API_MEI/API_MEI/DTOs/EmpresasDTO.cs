using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_MEI.Models;

namespace API_MEI.DTOs
{
    public class EmpresasDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da empresa deve ter no máximo 200 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O local da empresa é obrigatório.")]
        [StringLength(255, ErrorMessage = "O local da empresa deve ter no máximo 255 caracteres.")]
        public string Local { get; set; }

        [StringLength(255, ErrorMessage = "O local da empresa deve ter no máximo 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O email da empresa deve ser um endereço de email válido.")]
        public string? Email_empresa { get; set; }

        [StringLength(255, ErrorMessage = "O Protocolo deve ter no máximo 255 caracteres.")]
        public string? Protocolo { get; set; }
    }
}