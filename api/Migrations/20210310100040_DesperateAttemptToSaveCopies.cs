using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class DesperateAttemptToSaveCopies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Issues_IssueId",
                table: "Copies");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Issues_IssueId",
                table: "Copies",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Issues_IssueId",
                table: "Copies");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Issues_IssueId",
                table: "Copies",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
