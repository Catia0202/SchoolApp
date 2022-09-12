using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class turmafk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "turmaId",
                table: "Aluno",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_turmaId",
                table: "Aluno",
                column: "turmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_turma_turmaId",
                table: "Aluno",
                column: "turmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_turma_turmaId",
                table: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_turmaId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "turmaId",
                table: "Aluno");
        }
    }
}
