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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email_empresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email_especialista = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Equipa_Orientadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Membros_Id = table.Column<int>(type: "int", nullable: false),
                    Especialista_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipa_Orientadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipa_Orientadores_Especialistas_Especialista_Id",
                        column: x => x.Especialista_Id,
                        principalTable: "Especialistas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Membros",
                columns: table => new
                {
                    Equipa_Orientadores_Id = table.Column<int>(type: "int", nullable: false),
                    Docente_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membros", x => new { x.Equipa_Orientadores_Id, x.Docente_Id });
                    table.ForeignKey(
                        name: "FK_Membros_Docentes_Docente_Id",
                        column: x => x.Docente_Id,
                        principalTable: "Docentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membros_Equipa_Orientadores_Equipa_Orientadores_Id",
                        column: x => x.Equipa_Orientadores_Id,
                        principalTable: "Equipa_Orientadores",
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
                name: "Trabalho_Empresa",
                columns: table => new
                {
                    Trabalho_Id = table.Column<int>(type: "int", nullable: false),
                    Empresa_Id = table.Column<int>(type: "int", nullable: false),
                    Protocolo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalho_Empresa", x => new { x.Trabalho_Id, x.Empresa_Id });
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

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Curso", "Email", "Estado", "Instituição", "Nome", "Numero_Aluno" },
                values: new object[,]
                {
                    { 1, "Administração", "sofia.silva@gmail.com", true, "Universidade Federal do Rio Grande do Sul", "Sofia Silva", 845732 },
                    { 2, "Direito", "lucas.oliveira@yahoo.com", true, "Universidade de Brasília", "Lucas Oliveira", 678123 },
                    { 3, "Engenharia Elétrica", "ap.costa@hotmail.com", false, "Universidade Federal de Pernambuco", "Ana Paula Costa", 923456 },
                    { 4, "Medicina", "pedro.santos@gmail.com", true, "Universidade de São Paulo", "Pedro Santos", 346782 },
                    { 5, "Ciências da Computação", "julia.ferreira@yahoo.com", true, "Universidade Federal de Minas Gerais", "Julia Ferreira", 812394 },
                    { 6, "Psicologia", "rafaela.souza@hotmail.com", false, "Universidade Federal do Paraná", "Rafaela Souza", 473829 },
                    { 7, "Engenharia Mecânica", "marcos.oliveira@gmail.com", true, "Universidade Estadual de Campinas", "Marcos Oliveira", 912345 },
                    { 8, "Design Gráfico", "carla.rodrigues@yahoo.com", true, "Universidade Federal do Rio de Janeiro", "Carla Rodrigues", 567890 },
                    { 9, "Engenharia Civil", "gustavo.mendes@hotmail.com", false, "Universidade de São Paulo", "Gustavo Mendes", 234567 },
                    { 10, "Letras", "fernanda.oliveira@gmail.com", true, "Universidade Federal de Santa Catarina", "Fernanda Oliveira", 789012 },
                    { 11, "Ciências Contábeis", "jose.santos@gmail.com", true, "Universidade Federal do Ceará", "José Santos", 473829 },
                    { 12, "Engenharia Química", "amanda.silva@hotmail.com", false, "Universidade de São Paulo", "Amanda Silva", 923456 },
                    { 13, "Arquitetura", "mariana.souza@yahoo.com", true, "Universidade Federal do Paraná", "Mariana Souza", 845732 },
                    { 14, "Administração", "paulo.santos@gmail.com", false, "Universidade de Brasília", "Paulo Santos", 678123 },
                    { 15, "Direito", "gabriel.oliveira@hotmail.com", true, "Universidade Federal de Pernambuco", "Gabriel Oliveira", 346782 },
                    { 16, "Medicina Veterinária", "bruna.ferreira@yahoo.com", true, "Universidade de São Paulo", "Bruna Ferreira", 812394 },
                    { 17, "Engenharia de Produção", "carlos.oliveira@gmail.com", false, "Universidade Federal de Minas Gerais", "Carlos Oliveira", 912345 },
                    { 18, "Psicologia", "laura.rodrigues@hotmail.com", true, "Universidade Federal do Rio Grande do Sul", "Laura Rodrigues", 567890 },
                    { 19, "Engenharia de Computação", "fabio.mendes@gmail.com", true, "Universidade Estadual de Campinas", "Fábio Mendes", 234567 },
                    { 20, "Design de Interiores", "isabella.oliveira@yahoo.com", false, "Universidade Federal de Santa Catarina", "Isabella Oliveira", 789012 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipa_Orientadores_Especialista_Id",
                table: "Equipa_Orientadores",
                column: "Especialista_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Especialistas_Empresa_ID",
                table: "Especialistas",
                column: "Empresa_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Membros_Docente_Id",
                table: "Membros",
                column: "Docente_Id");

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
                name: "Trabalho");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Equipa_Orientadores");

            migrationBuilder.DropTable(
                name: "Juri");

            migrationBuilder.DropTable(
                name: "Especialistas");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
