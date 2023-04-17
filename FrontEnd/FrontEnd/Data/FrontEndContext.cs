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

        public DbSet<FrontEnd.Models.Trabalho> Trabalho { get; set; } = default!;
        public DbSet<FrontEnd.Models.Alunos>? Alunos { get; set; }
        public DbSet<FrontEnd.Models.Docentes> Docentes { get; set; } = default!;
        public DbSet<FrontEnd.Models.Empresas> Empresas { get; set; } = default!;
        public DbSet<FrontEnd.Models.Orientadores> Orientadores { get; set; } = default!;
        public DbSet<FrontEnd.Models.Especialistas> Especialistas { get; set; } = default!;
        public DbSet<FrontEnd.Models.Juri> Juri { get; set; } = default!;
        public DbSet<FrontEnd.Models.Membros> Membros { get; set; } = default!;
        public DbSet<FrontEnd.Models.JuriMembros> JuriMembros { get; set; } = default!;
        public DbSet<FrontEnd.Models.OrientadoresMembros> OrientadoresMembros { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JuriMembros>()
                .HasKey(o => new { o.Juri_Id, o.Membros_Id });

            modelBuilder.Entity<OrientadoresMembros>()
                .HasKey(o => new { o.OrientadorId, o.MembrosId });

        }

    }
}
