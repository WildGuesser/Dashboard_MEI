using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_MEI.Migrations
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_Aluno = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Curso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instituição = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filiacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipa_Orientadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orientador1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orientador2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orientador3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipa_Orientadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Juri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Presidente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vogal_Arguente1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vogal_Arguente2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vogal_Orientador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Defesa = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especialistas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empresa_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Especialistas_Empresas_Empresa_ID",
                        column: x => x.Empresa_ID,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabalho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alunos_Id = table.Column<int>(type: "int", nullable: false),
                    Juri_Id = table.Column<int>(type: "int", nullable: false),
                    Equipa_Orientadores_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabalho_Alunos_Alunos_Id",
                        column: x => x.Alunos_Id,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabalho_Equipa_Orientadores_Equipa_Orientadores_Id",
                        column: x => x.Equipa_Orientadores_Id,
                        principalTable: "Equipa_Orientadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabalho_Juri_Juri_Id",
                        column: x => x.Juri_Id,
                        principalTable: "Juri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Equipa_Orientadores_Id = table.Column<int>(type: "int", nullable: false),
                    Docente_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Especialista_Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membros_Docentes_Docente_Id",
                        column: x => x.Docente_Id,
                        principalTable: "Docentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Membros_Equipa_Orientadores_Equipa_Orientadores_Id",
                        column: x => x.Equipa_Orientadores_Id,
                        principalTable: "Equipa_Orientadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membros_Especialistas_Especialista_Id",
                        column: x => x.Especialista_Id,
                        principalTable: "Especialistas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trabalho_Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Protocolo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empresa_Id = table.Column<int>(type: "int", nullable: false),
                    Trabalho_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalho_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabalho_Empresa_Empresas_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabalho_Empresa_Trabalho_Trabalho_Id",
                        column: x => x.Trabalho_Id,
                        principalTable: "Trabalho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Especialistas_Empresa_ID",
                table: "Especialistas",
                column: "Empresa_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Membros_Docente_Id",
                table: "Membros",
                column: "Docente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Membros_Equipa_Orientadores_Id",
                table: "Membros",
                column: "Equipa_Orientadores_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Membros_Especialista_Id",
                table: "Membros",
                column: "Especialista_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Alunos_Id",
                table: "Trabalho",
                column: "Alunos_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Equipa_Orientadores_Id",
                table: "Trabalho",
                column: "Equipa_Orientadores_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Juri_Id",
                table: "Trabalho",
                column: "Juri_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Empresa_Empresa_Id",
                table: "Trabalho_Empresa",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Empresa_Trabalho_Id",
                table: "Trabalho_Empresa",
                column: "Trabalho_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Membros");

            migrationBuilder.DropTable(
                name: "Trabalho_Empresa");

            migrationBuilder.DropTable(
                name: "Docentes");

            migrationBuilder.DropTable(
                name: "Especialistas");

            migrationBuilder.DropTable(
                name: "Trabalho");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Equipa_Orientadores");

            migrationBuilder.DropTable(
                name: "Juri");
        }
    }
}
