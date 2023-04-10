using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using API_MEI.Models;

namespace FrontEnd.Data
{
    public class FrontEndContext : DbContext
    {
        public FrontEndContext (DbContextOptions<FrontEndContext> options)
            : base(options)
        {
        }

        public DbSet<FrontEnd.Models.Alunos> Alunos { get; set; } = default!;

        public DbSet<API_MEI.Models.Juri>? Juri { get; set; }

        public DbSet<API_MEI.Models.Docentes>? Docentes { get; set; }
    }
}
