using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class newfaltaclasscomponents4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Disciplina",
                newName: "Duracao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duracao",
                table: "Disciplina",
                newName: "Duration");
        }
    }
}
