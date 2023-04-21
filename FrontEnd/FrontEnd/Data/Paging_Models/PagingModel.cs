using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FrontEnd.Data.Paging_Models
{
    public class PagingModel
    {
        public int P { get; set; } = 1;
        public int S { get; set; } = 10;
        public int TotalRecords { get; set; } = 0;
    }
}
