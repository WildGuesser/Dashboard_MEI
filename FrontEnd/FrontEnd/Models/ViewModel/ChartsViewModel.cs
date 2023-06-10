using System.Text.Json.Serialization;

namespace FrontEnd.Models.ViewModel
{
	public class ChartsViewModel
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual List<Alunos>? Alunos { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual List<Trabalhos>? Trabalhos { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

		public virtual int? Nalunos { get; set; }

		public virtual int? NTrabalhos { get; set; }

		public virtual int? Nmenbros { get; set; }

        public int? Nempresas { get; set; }



        public virtual DateTime? DataMaisProxima { get; set; }

		public virtual string[]? Tipos { get; set; }

		public virtual int[]? TipoN { get; set; }

		public virtual int[]? Anos { get; set; }

		public virtual int[]? AnosN { get; set; }

		public virtual string[]? Intituicao { get; set; }

		public virtual int[]? IntituicaoN { get; set; }

        public virtual double[]? meanNotaPerAno { get; set; }
        public virtual int[]? meanNotaAnos { get; set; }

        public double[]? Pdefendidos { get; set; }

        public int[]? Ndefendidos { get; set; }

    }
}
