using FrontEnd.Models;

namespace FrontEnd.Data.Paging_Models
{
    public class EspecialistasPagingModel : PagingModel
    {
        public Especialistas Especialista { get; set; } = new Especialistas();
        public IList<Especialistas> EspecialistasList { get; set; } = new List<Especialistas>();
    }
}
