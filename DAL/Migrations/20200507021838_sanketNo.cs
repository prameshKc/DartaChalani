using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class sanketNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sanketNo",
                table: "tblFieldRekhankanDarta",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sanketNo",
                table: "tblFieldRekhankanDarta");
        }
    }
}
