using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class sitesetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblSiteSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSiteSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficeName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    siteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficeName_tblSiteSetting_siteId",
                        column: x => x.siteId,
                        principalTable: "tblSiteSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfficeName_siteId",
                table: "OfficeName",
                column: "siteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfficeName");

            migrationBuilder.DropTable(
                name: "tblSiteSetting");
        }
    }
}
