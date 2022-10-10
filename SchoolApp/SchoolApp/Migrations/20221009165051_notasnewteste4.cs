using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class notasnewteste4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nota_turma_TurmaId",
                table: "Nota");

            migrationBuilder.RenameColumn(
                name: "TurmaId",
                table: "Nota",
                newName: "turmaId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_TurmaId",
                table: "Nota",
                newName: "IX_Nota_turmaId");

            migrationBuilder.AddColumn<int>(
                name: "idaluno",
                table: "Nota",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_turma_turmaId",
                table: "Nota",
                column: "turmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nota_turma_turmaId",
                table: "Nota");

            migrationBuilder.DropColumn(
                name: "idaluno",
                table: "Nota");

            migrationBuilder.RenameColumn(
                name: "turmaId",
                table: "Nota",
                newName: "TurmaId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_turmaId",
                table: "Nota",
                newName: "IX_Nota_TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_turma_TurmaId",
                table: "Nota",
                column: "TurmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
