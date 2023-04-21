using FrontEnd.Models;

namespace FrontEnd.Data.Paging_Models
{
    public class DocentesPagingModel : PagingModel
    {
        public Docentes Docente { get; set; } = new Docentes();
        public IList<Docentes> DocentesList { get; set; } = new List<Docentes>();    
 
    }
}
