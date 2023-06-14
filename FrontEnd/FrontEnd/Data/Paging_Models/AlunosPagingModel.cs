using FrontEnd.Models;

namespace FrontEnd.Data.Paging_Models
{
    public class AlunosPagingModel : PagingModel
    {
        public Alunos Aluno { get; set; } = new Alunos();
        public IList<Alunos> AlunosList { get; set; } = new List <Alunos>();  
    }
}
