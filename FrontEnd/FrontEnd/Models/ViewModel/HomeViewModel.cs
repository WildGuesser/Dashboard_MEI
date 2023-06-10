using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FrontEnd.Models.ViewModel
{
	public class HomeViewModel
	{

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual List<Alunos>? Alunos { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<Trabalhos>? Trabalhos { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

		public virtual int? Nalunos { get; set; }

		public virtual int? NTrabalhos { get; set; }

		public virtual int? Nmenbros { get; set; }

        public virtual int? Nempresas { get; set; }

        public virtual DateTime? DataMaisProxima {get; set;}

		public virtual string[]? Tipos { get; set; }	

		public virtual int[]? TipoN { get;set; }

		public virtual int[]? Anos { get; set; }

		public virtual int[]? AnosN { get; set; }

	}
}
