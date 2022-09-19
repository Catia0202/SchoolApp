using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class newClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Duracao",
                table: "turma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Disciplina",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "turma");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Disciplina");

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
    }
}
