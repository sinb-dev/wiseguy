using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class MaillistNameToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Copies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Copies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Copies");
        }
    }
}
