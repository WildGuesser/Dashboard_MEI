using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_MEI.Models;
using System.Reflection.Emit;

namespace API_MEI.Data
{
    public class API_MEIContext : DbContext
    {
        public API_MEIContext(DbContextOptions<API_MEIContext> options)
            : base(options)
        {
        }


        public DbSet<API_MEI.Models.Trabalho> Trabalho { get; set; } = default!;
        public DbSet<API_MEI.Models.Alunos>? Alunos { get; set; }
        public DbSet<API_MEI.Models.Docentes> Docentes { get; set; } = default!;
        public DbSet<API_MEI.Models.Empresas> Empresas { get; set; } = default!;
        public DbSet<API_MEI.Models.Equipa_Orientadores> Equipa_Orientadores { get; set; } = default!;
        public DbSet<API_MEI.Models.Especialistas> Especialistas { get; set; } = default!;
        public DbSet<API_MEI.Models.Juri> Juri { get; set; } = default!;
        public DbSet<API_MEI.Models.Membros> Membros { get; set; } = default!;
        public DbSet<API_MEI.Models.Trabalho_Empresa> Trabalho_Empresa { get; set; } = default!;
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Membros>()
                .HasKey(o => new { o.Equipa_Orientadores_Id, o.Docente_Id });

            modelBuilder.Entity<Trabalho_Empresa>()
                .HasKey(o => new { o.Trabalho_Id, o.Empresa_Id });

            modelBuilder.ApplyConfiguration(new AlunoConfiguration());
            // add more configurations for other entities as needed
        }
    }
}
