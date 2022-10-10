using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class notasnewteste9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "alunoId",
                table: "Nota",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nota_alunoId",
                table: "Nota",
                column: "alunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Aluno_alunoId",
                table: "Nota",
                column: "alunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Aluno_alunoId",
                table: "Nota");

            migrationBuilder.DropIndex(
                name: "IX_Nota_alunoId",
                table: "Nota");

            migrationBuilder.DropColumn(
                name: "alunoId",
                table: "Nota");
        }
    }
}
