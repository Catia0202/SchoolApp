using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class newclassaluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_turma_turmaId",
                table: "Aluno");

            migrationBuilder.RenameColumn(
                name: "turmaId",
                table: "Aluno",
                newName: "turmaid");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_turmaId",
                table: "Aluno",
                newName: "IX_Aluno_turmaid");

            migrationBuilder.AlterColumn<int>(
                name: "turmaid",
                table: "Aluno",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_turma_turmaid",
                table: "Aluno",
                column: "turmaid",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_turma_turmaid",
                table: "Aluno");

            migrationBuilder.RenameColumn(
                name: "turmaid",
                table: "Aluno",
                newName: "turmaId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_turmaid",
                table: "Aluno",
                newName: "IX_Aluno_turmaId");

            migrationBuilder.AlterColumn<int>(
                name: "turmaId",
                table: "Aluno",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_turma_turmaId",
                table: "Aluno",
                column: "turmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
