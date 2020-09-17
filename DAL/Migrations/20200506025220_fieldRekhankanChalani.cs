using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class fieldRekhankanChalani : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFieldRekhankanChalani",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChalaniNo = table.Column<string>(nullable: true),
                    LetterCount = table.Column<int>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    isTicket = table.Column<bool>(nullable: false),
                    Information = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    wardNo = table.Column<string>(nullable: true),
                    chalanMiti = table.Column<string>(nullable: true),
                    patraMiti = table.Column<string>(nullable: true),
                    chalanId = table.Column<int>(nullable: false),
                    subjectId = table.Column<int>(nullable: false),
                    TotalLetterCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFieldRekhankanChalani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFieldRekhankanChalani_tblChalan_chalanId",
                        column: x => x.chalanId,
                        principalTable: "tblChalan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFieldRekhankanChalani_tblSubject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "tblSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRekhankanChalanFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRekhankanChalanFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFiledRekhakanChalanPatras",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fieldRekhankanChalaniId = table.Column<int>(nullable: false),
                    fileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFiledRekhakanChalanPatras", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblFiledRekhakanChalanPatras_tblFieldRekhankanChalani_fieldRekhankanChalaniId",
                        column: x => x.fieldRekhankanChalaniId,
                        principalTable: "tblFieldRekhankanChalani",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFiledRekhakanChalanPatras_tblRekhankanChalanFile_fileId",
                        column: x => x.fileId,
                        principalTable: "tblRekhankanChalanFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFieldRekhankanChalani_chalanId",
                table: "tblFieldRekhankanChalani",
                column: "chalanId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFieldRekhankanChalani_subjectId",
                table: "tblFieldRekhankanChalani",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFiledRekhakanChalanPatras_fieldRekhankanChalaniId",
                table: "tblFiledRekhakanChalanPatras",
                column: "fieldRekhankanChalaniId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFiledRekhakanChalanPatras_fileId",
                table: "tblFiledRekhakanChalanPatras",
                column: "fileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFiledRekhakanChalanPatras");

            migrationBuilder.DropTable(
                name: "tblFieldRekhankanChalani");

            migrationBuilder.DropTable(
                name: "tblRekhankanChalanFile");
        }
    }
}
