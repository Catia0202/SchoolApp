using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class newclassturma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "turma",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_turma_UserId",
                table: "turma",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_turma_AspNetUsers_UserId",
                table: "turma",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_turma_AspNetUsers_UserId",
                table: "turma");

            migrationBuilder.DropIndex(
                name: "IX_turma_UserId",
                table: "turma");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "turma");
        }
    }
}
