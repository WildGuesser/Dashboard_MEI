﻿using System;
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


        public DbSet<API_MEI.Models.Trabalhos> Trabalhos { get; set; } = default!;
        public DbSet<API_MEI.Models.Alunos>? Alunos { get; set; } = default!;
        public DbSet<API_MEI.Models.Docentes> Docentes { get; set; } = default!;
        public DbSet<API_MEI.Models.Empresas> Empresas { get; set; } = default!;
        public DbSet<API_MEI.Models.Orientadores> Orientadores { get; set; } = default!;
        public DbSet<API_MEI.Models.Especialistas> Especialistas { get; set; } = default!;
        public DbSet<API_MEI.Models.Juri> Juri { get; set; } = default!;
        public DbSet<API_MEI.Models.Membros> Membros { get; set; } = default!;
        public DbSet<API_MEI.Models.JuriMembros> JuriMembros { get; set; } = default!;

        public DbSet<API_MEI.Models.User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JuriMembros>()
                .HasKey(o => new { o.Juri_Id, o.Membro_Id });

            modelBuilder.Entity<Orientadores>()
                .HasKey(o => new { o.Trabalho_Id, o.Membro_Id });

        }

    }
}


