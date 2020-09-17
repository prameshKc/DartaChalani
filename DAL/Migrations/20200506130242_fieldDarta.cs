using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class fieldDarta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFieldRekhankanDarta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DartaNo = table.Column<string>(nullable: true),
                    LetterCount = table.Column<int>(nullable: false),
                    ApplicantName = table.Column<string>(nullable: true),
                    ParentName = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    wardNo = table.Column<string>(nullable: true),
                    KitaNo = table.Column<string>(nullable: true),
                    ChetraFal = table.Column<string>(nullable: true),
                    DartaMiti = table.Column<string>(nullable: true),
                    RekhankanMiti = table.Column<string>(nullable: true),
                    SecondRekhankanMiti = table.Column<string>(nullable: true),
                    ReasonOfReject = table.Column<string>(nullable: true),
                    TamiliMiti = table.Column<string>(nullable: true),
                    TotalLetterCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFieldRekhankanDarta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFieldRekhankanDartaFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFieldRekhankanDartaFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFieldRekhankanDartaPatra",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fieldRekhankanDartaId = table.Column<int>(nullable: false),
                    fileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFieldRekhankanDartaPatra", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblFieldRekhankanDartaPatra_tblFieldRekhankanDarta_fieldRekhankanDartaId",
                        column: x => x.fieldRekhankanDartaId,
                        principalTable: "tblFieldRekhankanDarta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFieldRekhankanDartaPatra_tblFieldRekhankanDartaFile_fileId",
                        column: x => x.fileId,
                        principalTable: "tblFieldRekhankanDartaFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFieldRekhankanDartaPatra_fieldRekhankanDartaId",
                table: "tblFieldRekhankanDartaPatra",
                column: "fieldRekhankanDartaId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFieldRekhankanDartaPatra_fileId",
                table: "tblFieldRekhankanDartaPatra",
                column: "fileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFieldRekhankanDartaPatra");

            migrationBuilder.DropTable(
                name: "tblFieldRekhankanDarta");

            migrationBuilder.DropTable(
                name: "tblFieldRekhankanDartaFile");
        }
    }
}
