using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class halsabikdarta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HalsabikDartaFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalsabikDartaFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HalsabikDartas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DartaNo = table.Column<string>(nullable: true),
                    MalpotChalanNo = table.Column<string>(nullable: true),
                    SabikKitaNo = table.Column<string>(nullable: true),
                    CurrentKitaNo = table.Column<string>(nullable: true),
                    FileSanketNo = table.Column<string>(nullable: true),
                    BoxPatra = table.Column<string>(nullable: true),
                    GiverName = table.Column<string>(nullable: true),
                    ReceiverName = table.Column<string>(nullable: true),
                    ParentName = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    wardNo = table.Column<string>(nullable: true),
                    DartaMiti = table.Column<string>(nullable: true),
                    SendMiti = table.Column<string>(nullable: true),
                    MalpotMiti = table.Column<string>(nullable: true),
                    TotalLetterCount = table.Column<int>(nullable: false),
                    MalpotLetterCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalsabikDartas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HalsabikDartaPatras",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HalsabikDartaId = table.Column<int>(nullable: false),
                    fileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalsabikDartaPatras", x => x.id);
                    table.ForeignKey(
                        name: "FK_HalsabikDartaPatras_HalsabikDartas_HalsabikDartaId",
                        column: x => x.HalsabikDartaId,
                        principalTable: "HalsabikDartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HalsabikDartaPatras_HalsabikDartaFiles_fileId",
                        column: x => x.fileId,
                        principalTable: "HalsabikDartaFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HalsabikDartaPatras_HalsabikDartaId",
                table: "HalsabikDartaPatras",
                column: "HalsabikDartaId");

            migrationBuilder.CreateIndex(
                name: "IX_HalsabikDartaPatras_fileId",
                table: "HalsabikDartaPatras",
                column: "fileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HalsabikDartaPatras");

            migrationBuilder.DropTable(
                name: "HalsabikDartas");

            migrationBuilder.DropTable(
                name: "HalsabikDartaFiles");
        }
    }
}
