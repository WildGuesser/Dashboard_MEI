using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_MEI.Data.Migrations
{
    public partial class changeJuri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabalhos_Juri_Juri_Id",
                table: "Trabalhos");

            migrationBuilder.DropIndex(
                name: "IX_Trabalhos_Juri_Id",
                table: "Trabalhos");

            migrationBuilder.DropColumn(
                name: "Juri_Id",
                table: "Trabalhos");

            migrationBuilder.AddColumn<int>(
                name: "Trabalho_Id",
                table: "Juri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Juri_Trabalho_Id",
                table: "Juri",
                column: "Trabalho_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Juri_Trabalhos_Trabalho_Id",
                table: "Juri",
                column: "Trabalho_Id",
                principalTable: "Trabalhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Juri_Trabalhos_Trabalho_Id",
                table: "Juri");

            migrationBuilder.DropIndex(
                name: "IX_Juri_Trabalho_Id",
                table: "Juri");

            migrationBuilder.DropColumn(
                name: "Trabalho_Id",
                table: "Juri");

            migrationBuilder.AddColumn<int>(
                name: "Juri_Id",
                table: "Trabalhos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trabalhos_Juri_Id",
                table: "Trabalhos",
                column: "Juri_Id",
                unique: true,
                filter: "[Juri_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabalhos_Juri_Juri_Id",
                table: "Trabalhos",
                column: "Juri_Id",
                principalTable: "Juri",
                principalColumn: "Id");
        }
    }
}
