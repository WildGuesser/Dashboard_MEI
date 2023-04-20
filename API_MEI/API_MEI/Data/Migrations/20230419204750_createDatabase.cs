using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_MEI.Data.Migrations
{
    public partial class createDatabase : Migration
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
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Curso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Instituicao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Local = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email_empresa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Protocolo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Juri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_Defesa = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trabalho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReferenciaInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdendaProtocolo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Aluno_Id = table.Column<int>(type: "int", nullable: false),
                    Juri_Id = table.Column<int>(type: "int", nullable: false),
                    Empresa_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabalho_Alunos_Aluno_Id",
                        column: x => x.Aluno_Id,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabalho_Empresas_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trabalho_Juri_Juri_Id",
                        column: x => x.Juri_Id,
                        principalTable: "Juri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Filiacao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Docentes_Membros_Id",
                        column: x => x.Id,
                        principalTable: "Membros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Especialistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Empresa_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Especialistas_Empresas_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Especialistas_Membros_Id",
                        column: x => x.Id,
                        principalTable: "Membros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JuriMembros",
                columns: table => new
                {
                    Juri_Id = table.Column<int>(type: "int", nullable: false),
                    Membro_Id = table.Column<int>(type: "int", nullable: false),
                    Funcao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuriMembros", x => new { x.Juri_Id, x.Membro_Id });
                    table.ForeignKey(
                        name: "FK_JuriMembros_Juri_Juri_Id",
                        column: x => x.Juri_Id,
                        principalTable: "Juri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JuriMembros_Membros_Membro_Id",
                        column: x => x.Membro_Id,
                        principalTable: "Membros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orientadores",
                columns: table => new
                {
                    Trabalho_Id = table.Column<int>(type: "int", nullable: false),
                    Membro_Id = table.Column<int>(type: "int", nullable: false),
                    Funcao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orientadores", x => new { x.Trabalho_Id, x.Membro_Id });
                    table.ForeignKey(
                        name: "FK_Orientadores_Membros_Membro_Id",
                        column: x => x.Membro_Id,
                        principalTable: "Membros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orientadores_Trabalho_Trabalho_Id",
                        column: x => x.Trabalho_Id,
                        principalTable: "Trabalho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Especialistas_Empresa_Id",
                table: "Especialistas",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JuriMembros_Membro_Id",
                table: "JuriMembros",
                column: "Membro_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orientadores_Membro_Id",
                table: "Orientadores",
                column: "Membro_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Aluno_Id",
                table: "Trabalho",
                column: "Aluno_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Empresa_Id",
                table: "Trabalho",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trabalho_Juri_Id",
                table: "Trabalho",
                column: "Juri_Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Docentes");

            migrationBuilder.DropTable(
                name: "Especialistas");

            migrationBuilder.DropTable(
                name: "JuriMembros");

            migrationBuilder.DropTable(
                name: "Orientadores");

            migrationBuilder.DropTable(
                name: "Membros");

            migrationBuilder.DropTable(
                name: "Trabalho");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Juri");
        }
    }
}
