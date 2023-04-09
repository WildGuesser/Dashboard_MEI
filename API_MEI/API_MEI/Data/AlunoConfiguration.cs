using API_MEI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace API_MEI
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Alunos>
    {
        public void Configure(EntityTypeBuilder<Alunos> builder)
        {
            builder.HasData(
                new Alunos
                {
                    Id = 1,
                    Numero_Aluno = 845732,
                    Nome = "Sofia Silva",
                    Curso = "Administração",
                    Email = "sofia.silva@gmail.com",
                    Instituição = "Universidade Federal do Rio Grande do Sul",
                    Estado = true
                },
                new Alunos
                {
                    Id = 2,
                    Numero_Aluno = 678123,
                    Nome = "Lucas Oliveira",
                    Curso = "Direito",
                    Email = "lucas.oliveira@yahoo.com",
                    Instituição = "Universidade de Brasília",
                    Estado = true
                },
                new Alunos
                {
                    Id = 3,
                    Numero_Aluno = 923456,
                    Nome = "Ana Paula Costa",
                    Curso = "Engenharia Elétrica",
                    Email = "ap.costa@hotmail.com",
                    Instituição = "Universidade Federal de Pernambuco",
                    Estado = false
                },
                new Alunos
                {
                    Id = 4,
                    Numero_Aluno = 346782,
                    Nome = "Pedro Santos",
                    Curso = "Medicina",
                    Email = "pedro.santos@gmail.com",
                    Instituição = "Universidade de São Paulo",
                    Estado = true
                },
                new Alunos
                {

                    Id = 5,
                    Numero_Aluno = 812394,
                    Nome = "Julia Ferreira",
                    Curso = "Ciências da Computação",
                    Email = "julia.ferreira@yahoo.com",
                    Instituição = "Universidade Federal de Minas Gerais",
                    Estado = true
                },
                new Alunos
                {

                    Id = 6,
                    Numero_Aluno = 473829,
                    Nome = "Rafaela Souza",
                    Curso = "Psicologia",
                    Email = "rafaela.souza@hotmail.com",
                    Instituição = "Universidade Federal do Paraná",
                    Estado = false
                },
                new Alunos
                {

                    Id = 7,
                    Numero_Aluno = 912345,
                    Nome = "Marcos Oliveira",
                    Curso = "Engenharia Mecânica",
                    Email = "marcos.oliveira@gmail.com",
                    Instituição = "Universidade Estadual de Campinas",
                    Estado = true
                },
                new Alunos
                {

                    Id = 8,
                    Numero_Aluno = 567890,
                    Nome = "Carla Rodrigues",
                    Curso = "Design Gráfico",
                    Email = "carla.rodrigues@yahoo.com",
                    Instituição = "Universidade Federal do Rio de Janeiro",
                    Estado = true
                },
                new Alunos
                {
                        
                    Id = 9,
                    Numero_Aluno = 234567,
                    Nome = "Gustavo Mendes",
                    Curso = "Engenharia Civil",
                    Email = "gustavo.mendes@hotmail.com",
                    Instituição = "Universidade de São Paulo",
                    Estado = false
                },
                new Alunos
                {

                    Id = 10,
                    Numero_Aluno = 789012,
                    Nome = "Fernanda Oliveira",
                    Curso = "Letras",
                    Email = "fernanda.oliveira@gmail.com",
                    Instituição = "Universidade Federal de Santa Catarina",
                    Estado = true
                },
                new Alunos
                {
                    Id = 11,
                    Numero_Aluno = 473829,
                    Nome = "José Santos",
                    Curso = "Ciências Contábeis",
                    Email = "jose.santos@gmail.com",
                    Instituição = "Universidade Federal do Ceará",
                    Estado = true
                },
                new Alunos
                {
                    Id = 12,
                    Numero_Aluno = 923456,
                    Nome = "Amanda Silva",
                    Curso = "Engenharia Química",
                    Email = "amanda.silva@hotmail.com",
                    Instituição = "Universidade de São Paulo",
                    Estado = false
                },
                new Alunos
                {
                    Id = 13,
                    Numero_Aluno = 845732,
                    Nome = "Mariana Souza",
                    Curso = "Arquitetura",
                    Email = "mariana.souza@yahoo.com",
                    Instituição = "Universidade Federal do Paraná",
                    Estado = true
                },
                new Alunos
                {
                    Id = 14,
                    Numero_Aluno = 678123,
                    Nome = "Paulo Santos",
                    Curso = "Administração",
                    Email = "paulo.santos@gmail.com",
                    Instituição = "Universidade de Brasília",
                    Estado = false
                },
                new Alunos
                {
                    Id = 15,
                    Numero_Aluno = 346782,
                    Nome = "Gabriel Oliveira",
                    Curso = "Direito",
                    Email = "gabriel.oliveira@hotmail.com",
                    Instituição = "Universidade Federal de Pernambuco",
                    Estado = true
                },
                new Alunos
                {
                    Id = 16,
                    Numero_Aluno = 812394,
                    Nome = "Bruna Ferreira",
                    Curso = "Medicina Veterinária",
                    Email = "bruna.ferreira@yahoo.com",
                    Instituição = "Universidade de São Paulo",
                    Estado = true
                },
                new Alunos
                {
                    Id = 17,
                    Numero_Aluno = 912345,
                    Nome = "Carlos Oliveira",
                    Curso = "Engenharia de Produção",
                    Email = "carlos.oliveira@gmail.com",
                    Instituição = "Universidade Federal de Minas Gerais",
                    Estado = false
                },
                new Alunos
                {
                    Id = 18,
                    Numero_Aluno = 567890,
                    Nome = "Laura Rodrigues",
                    Curso = "Psicologia",
                    Email = "laura.rodrigues@hotmail.com",
                    Instituição = "Universidade Federal do Rio Grande do Sul",
                    Estado = true
                },
                new Alunos
                {
                    Id = 19,
                    Numero_Aluno = 234567,
                    Nome = "Fábio Mendes",
                    Curso = "Engenharia de Computação",
                    Email = "fabio.mendes@gmail.com",
                    Instituição = "Universidade Estadual de Campinas",
                    Estado = true
                },
                new Alunos
                {
                    Id = 20,
                    Numero_Aluno = 789012,
                    Nome = "Isabella Oliveira",
                    Curso = "Design de Interiores",
                    Email = "isabella.oliveira@yahoo.com",
                    Instituição = "Universidade Federal de Santa Catarina",
                    Estado = false
                }

            );
        }
    }
}