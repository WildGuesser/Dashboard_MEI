using FrontEnd.Models;
using FrontEnd.Models.ViewModel;

namespace FrontEnd.Data.Paging_Models
{
    public class UsersPagingModel
    {
        public LogInViewModel User { get; set; } = new LogInViewModel();
        public IList<LogInViewModel> UserList { get; set; } = new List<LogInViewModel>();
    }
}
