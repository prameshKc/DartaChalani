using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChitthiPurjis_subjectId",
                table: "ChitthiPurjis");

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiPurjis_subjectId",
                table: "ChitthiPurjis",
                column: "subjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChitthiPurjis_subjectId",
                table: "ChitthiPurjis");

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiPurjis_subjectId",
                table: "ChitthiPurjis",
                column: "subjectId",
                unique: true);
        }
    }
}
