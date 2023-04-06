using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;

namespace FrontEnd.Data
{
    public class FrontEndContext : DbContext
    {
        public FrontEndContext (DbContextOptions<FrontEndContext> options)
            : base(options)
        {
        }

        public DbSet<FrontEnd.Models.Alunos> Alunos { get; set; } = default!;
    }
}
