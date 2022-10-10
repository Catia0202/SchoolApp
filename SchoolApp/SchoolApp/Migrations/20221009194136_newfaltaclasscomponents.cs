using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class newfaltaclasscomponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "duracao",
                table: "falta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duracao",
                table: "falta");
        }
    }
}
