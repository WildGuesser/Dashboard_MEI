using System.Text.Json.Serialization;

namespace API_MEI.Models
{
    public class Calcs
    {

        [JsonPropertyName("nalunos")]
        public int? Nalunos { get; set; }

        [JsonPropertyName("ntrabalhos")]
        public int? NTrabalhos { get; set; }

        [JsonPropertyName("nmenbros")]
        public int? Nmenbros { get; set; }

        [JsonPropertyName("dataMaisProxima")]
        public DateTime? DataMaisProxima { get; set; }
    }
}
