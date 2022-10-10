using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class newfaltaclasscomponents2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dia",
                table: "falta");

            migrationBuilder.DropColumn(
                name: "Hora",
                table: "falta");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "falta",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "falta");

            migrationBuilder.AddColumn<string>(
                name: "Dia",
                table: "falta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hora",
                table: "falta",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
