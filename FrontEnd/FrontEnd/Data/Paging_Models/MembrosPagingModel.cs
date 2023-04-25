using FrontEnd.Models;

namespace FrontEnd.Data.Paging_Models
{
    public class MembrosPagingModel : PagingModel
    {
        public Membros Membro { get; set; } = new Membros();
        public IList<Membros> MembrosList { get; set; } = new List<Membros>();
    }
}
