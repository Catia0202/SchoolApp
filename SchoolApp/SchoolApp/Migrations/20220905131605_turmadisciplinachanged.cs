using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class turmadisciplinachanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_turma_Disciplina_cod_disciplinaId",
                table: "turma");

            migrationBuilder.DropIndex(
                name: "IX_turma_cod_disciplinaId",
                table: "turma");

            migrationBuilder.DropColumn(
                name: "cod_disciplinaId",
                table: "turma");

            migrationBuilder.AddColumn<int>(
                name: "cod_turmaId",
                table: "Disciplina",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_cod_turmaId",
                table: "Disciplina",
                column: "cod_turmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplina_turma_cod_turmaId",
                table: "Disciplina",
                column: "cod_turmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplina_turma_cod_turmaId",
                table: "Disciplina");

            migrationBuilder.DropIndex(
                name: "IX_Disciplina_cod_turmaId",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "cod_turmaId",
                table: "Disciplina");

            migrationBuilder.AddColumn<int>(
                name: "cod_disciplinaId",
                table: "turma",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_turma_cod_disciplinaId",
                table: "turma",
                column: "cod_disciplinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_turma_Disciplina_cod_disciplinaId",
                table: "turma",
                column: "cod_disciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
