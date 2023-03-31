using System;

namespace API_MEI.DTOs
{
    public class JuriDTO
    {
        public int Id { get; set; }
        public string Presidente { get; set; }
        public string Vogal_Arguente1 { get; set; }
        public string Vogal_Arguente2 { get; set; }
        public string Vogal_Orientador { get; set; }
        public DateTime Data_Defesa { get; set; }
    }
}