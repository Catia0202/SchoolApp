using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class faltanewteste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_falta_Aluno_cod_alunoId",
                table: "falta");

            migrationBuilder.DropForeignKey(
                name: "FK_falta_Disciplina_cod_disciplinaId",
                table: "falta");

            migrationBuilder.DropIndex(
                name: "IX_falta_cod_alunoId",
                table: "falta");

            migrationBuilder.DropIndex(
                name: "IX_falta_cod_disciplinaId",
                table: "falta");

            migrationBuilder.DropColumn(
                name: "cod_alunoId",
                table: "falta");

            migrationBuilder.DropColumn(
                name: "cod_disciplinaId",
                table: "falta");

            migrationBuilder.AddColumn<int>(
                name: "alunoid",
                table: "falta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "disciplinaid",
                table: "falta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_falta_alunoid",
                table: "falta",
                column: "alunoid");

            migrationBuilder.CreateIndex(
                name: "IX_falta_disciplinaid",
                table: "falta",
                column: "disciplinaid");

            migrationBuilder.AddForeignKey(
                name: "FK_falta_Aluno_alunoid",
                table: "falta",
                column: "alunoid",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_falta_Disciplina_disciplinaid",
                table: "falta",
                column: "disciplinaid",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_falta_Aluno_alunoid",
                table: "falta");

            migrationBuilder.DropForeignKey(
                name: "FK_falta_Disciplina_disciplinaid",
                table: "falta");

            migrationBuilder.DropIndex(
                name: "IX_falta_alunoid",
                table: "falta");

            migrationBuilder.DropIndex(
                name: "IX_falta_disciplinaid",
                table: "falta");

            migrationBuilder.DropColumn(
                name: "alunoid",
                table: "falta");

            migrationBuilder.DropColumn(
                name: "disciplinaid",
                table: "falta");

            migrationBuilder.AddColumn<int>(
                name: "cod_alunoId",
                table: "falta",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cod_disciplinaId",
                table: "falta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_falta_cod_alunoId",
                table: "falta",
                column: "cod_alunoId");

            migrationBuilder.CreateIndex(
                name: "IX_falta_cod_disciplinaId",
                table: "falta",
                column: "cod_disciplinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_falta_Aluno_cod_alunoId",
                table: "falta",
                column: "cod_alunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_falta_Disciplina_cod_disciplinaId",
                table: "falta",
                column: "cod_disciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
