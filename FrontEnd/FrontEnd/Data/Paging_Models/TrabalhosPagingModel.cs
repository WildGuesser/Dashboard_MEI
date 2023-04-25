using FrontEnd.Models;

namespace FrontEnd.Data.Paging_Models
{
    public class TrabalhosPagingModel : PagingModel
    {
        public Trabalhos Trabalho { get; set; } = new Trabalhos();
        public IList<Trabalhos> TrabalhosList { get; set; } = new List<Trabalhos>();

        public Orientadores Orientadores { get; set; } 

    }
}
