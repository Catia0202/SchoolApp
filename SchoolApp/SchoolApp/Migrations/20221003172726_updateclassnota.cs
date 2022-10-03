using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class updateclassnota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Aluno_cod_alunoId",
                table: "Nota");

            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Disciplina_cod_disciplinaId",
                table: "Nota");

            migrationBuilder.RenameColumn(
                name: "cod_disciplinaId",
                table: "Nota",
                newName: "disciplinaId");

            migrationBuilder.RenameColumn(
                name: "cod_alunoId",
                table: "Nota",
                newName: "alunoId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_cod_disciplinaId",
                table: "Nota",
                newName: "IX_Nota_disciplinaId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_cod_alunoId",
                table: "Nota",
                newName: "IX_Nota_alunoId");

            migrationBuilder.AlterColumn<int>(
                name: "NotaAluno",
                table: "Nota",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Nota",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "id_aluno",
                table: "Nota",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_disciplina",
                table: "Nota",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Aluno_alunoId",
                table: "Nota",
                column: "alunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Disciplina_disciplinaId",
                table: "Nota",
                column: "disciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Aluno_alunoId",
                table: "Nota");

            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Disciplina_disciplinaId",
                table: "Nota");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Nota");

            migrationBuilder.DropColumn(
                name: "id_aluno",
                table: "Nota");

            migrationBuilder.DropColumn(
                name: "id_disciplina",
                table: "Nota");

            migrationBuilder.RenameColumn(
                name: "disciplinaId",
                table: "Nota",
                newName: "cod_disciplinaId");

            migrationBuilder.RenameColumn(
                name: "alunoId",
                table: "Nota",
                newName: "cod_alunoId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_disciplinaId",
                table: "Nota",
                newName: "IX_Nota_cod_disciplinaId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_alunoId",
                table: "Nota",
                newName: "IX_Nota_cod_alunoId");

            migrationBuilder.AlterColumn<string>(
                name: "NotaAluno",
                table: "Nota",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Aluno_cod_alunoId",
                table: "Nota",
                column: "cod_alunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Disciplina_cod_disciplinaId",
                table: "Nota",
                column: "cod_disciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
