using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class updateclassnota2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "Nota",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_turma",
                table: "Nota",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Nota_TurmaId",
                table: "Nota",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_turma_TurmaId",
                table: "Nota",
                column: "TurmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nota_turma_TurmaId",
                table: "Nota");

            migrationBuilder.DropIndex(
                name: "IX_Nota_TurmaId",
                table: "Nota");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Nota");

            migrationBuilder.DropColumn(
                name: "id_turma",
                table: "Nota");
        }
    }
}
