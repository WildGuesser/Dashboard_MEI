using FrontEnd.Models;

namespace FrontEnd.Data.Paging_Models
{
    public class EmpresaPagingModel : PagingModel
    {
        public Empresas Empresas { get; set; } = new Empresas();
        public IList<Empresas> EmpresaList { get; set; } = new List<Empresas>();    
    }
}
