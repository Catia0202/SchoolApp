using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class notanewtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_turma",
                table: "Nota",
                newName: "idturma");

            migrationBuilder.RenameColumn(
                name: "id_disciplina",
                table: "Nota",
                newName: "iddisciplina");

            migrationBuilder.RenameColumn(
                name: "id_aluno",
                table: "Nota",
                newName: "idaluno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idturma",
                table: "Nota",
                newName: "id_turma");

            migrationBuilder.RenameColumn(
                name: "iddisciplina",
                table: "Nota",
                newName: "id_disciplina");

            migrationBuilder.RenameColumn(
                name: "idaluno",
                table: "Nota",
                newName: "id_aluno");
        }
    }
}
