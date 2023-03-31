using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_MEI.Models;

namespace API_MEI.Data
{
    public class API_MEIContext : DbContext
    {
        public API_MEIContext (DbContextOptions<API_MEIContext> options)
            : base(options)
        {
        }

        public DbSet<API_MEI.Models.Trabalho> Trabalho { get; set; } = default!;
        public DbSet<API_MEI.Models.Docentes> Docentes { get; set; } = default!;
        public DbSet<API_MEI.Models.Empresas> Empresas { get; set; } = default!;
        public DbSet<API_MEI.Models.Equipa_Orientadores> Equipa_Orientadores { get; set; } = default!;
        public DbSet<API_MEI.Models.Especialistas> Especialistas { get; set; } = default!;
        public DbSet<API_MEI.Models.Juri> Juri { get; set; } = default!;
        public DbSet<API_MEI.Models.Membros> Membros { get; set; } = default!;
        public DbSet<API_MEI.Models.Trabalho_Empresa> Trabalho_Empresa { get; set; } = default!;
        public DbSet<API_MEI.Models.Alunos>? Alunos { get; set; }

    }
}
