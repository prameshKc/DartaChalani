using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class l : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChalanPatras_ChalanFiles_fileId",
                table: "ChalanPatras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChalanFiles",
                table: "ChalanFiles");

            migrationBuilder.RenameTable(
                name: "ChalanFiles",
                newName: "Files");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanPatras_Files_fileId",
                table: "ChalanPatras",
                column: "fileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChalanPatras_Files_fileId",
                table: "ChalanPatras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "ChalanFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChalanFiles",
                table: "ChalanFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanPatras_ChalanFiles_fileId",
                table: "ChalanPatras",
                column: "fileId",
                principalTable: "ChalanFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
