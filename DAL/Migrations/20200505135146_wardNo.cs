using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class wardNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "wardNo",
                table: "ChitthiPurjis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wardNo",
                table: "chitthiPurjiDartas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wardNo",
                table: "ChitthiPurjis");

            migrationBuilder.DropColumn(
                name: "wardNo",
                table: "chitthiPurjiDartas");
        }
    }
}
