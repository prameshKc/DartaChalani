using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class prefixType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "tblPrefix",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "tblPrefix");
        }
    }
}
