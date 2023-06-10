using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models.ViewModel
{
	public class HomeViewModel
	{

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[ForeignKey("Aluno_Id")]
		[InverseProperty("Trabalho")]
		public virtual List<Alunos>? Alunos { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

		public virtual int? Nalunos { get; set; }

		public virtual int? NTrabalhos { get; set; }

		public virtual int? Nmenbros { get; set; }

		public virtual DateTime? DataMaisProxima {get; set;}



    }
}
