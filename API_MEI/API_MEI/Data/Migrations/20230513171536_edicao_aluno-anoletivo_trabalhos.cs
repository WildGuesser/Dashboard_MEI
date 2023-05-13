using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_MEI.Data.Migrations
{
    public partial class edicao_alunoanoletivo_trabalhos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ano_Letivo",
                table: "Trabalhos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Defesa",
                table: "Juri",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Edicao",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano_Letivo",
                table: "Trabalhos");

            migrationBuilder.DropColumn(
                name: "Edicao",
                table: "Alunos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Defesa",
                table: "Juri",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
