using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class nameCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficeName_tblSiteSetting_siteId",
                table: "OfficeName");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeName_tblSiteSetting_siteId",
                table: "OfficeName",
                column: "siteId",
                principalTable: "tblSiteSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficeName_tblSiteSetting_siteId",
                table: "OfficeName");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeName_tblSiteSetting_siteId",
                table: "OfficeName",
                column: "siteId",
                principalTable: "tblSiteSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
