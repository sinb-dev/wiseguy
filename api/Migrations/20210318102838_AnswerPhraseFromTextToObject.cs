using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class AnswerPhraseFromTextToObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Copies_SheetId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Phrase",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "PhraseId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PhraseId",
                table: "Answers",
                column: "PhraseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Copies_SheetId",
                table: "Answers",
                column: "SheetId",
                principalTable: "Copies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Phrases_PhraseId",
                table: "Answers",
                column: "PhraseId",
                principalTable: "Phrases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Copies_SheetId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Phrases_PhraseId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_PhraseId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "PhraseId",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "Phrase",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Copies_SheetId",
                table: "Answers",
                column: "SheetId",
                principalTable: "Copies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
