using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class CascadeDeletePhrase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phrases_Templates_SheetTemplateId",
                table: "Phrases");

            migrationBuilder.AddForeignKey(
                name: "FK_Phrases_Templates_SheetTemplateId",
                table: "Phrases",
                column: "SheetTemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phrases_Templates_SheetTemplateId",
                table: "Phrases");

            migrationBuilder.AddForeignKey(
                name: "FK_Phrases_Templates_SheetTemplateId",
                table: "Phrases",
                column: "SheetTemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
