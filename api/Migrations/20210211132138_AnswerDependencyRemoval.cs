using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class AnswerDependencyRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Templates_SheetTemplateId",
                table: "Copies");

            migrationBuilder.RenameColumn(
                name: "PhraseId",
                table: "Answers",
                newName: "AnswerId");

            migrationBuilder.AddColumn<string>(
                name: "Phrase",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Templates_SheetTemplateId",
                table: "Copies",
                column: "SheetTemplateId",
                principalTable: "Templates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Templates_SheetTemplateId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "Phrase",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "Answers",
                newName: "PhraseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Templates_SheetTemplateId",
                table: "Copies",
                column: "SheetTemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
