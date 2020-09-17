using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class halsabikchalani : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HalsabikChalaniFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalsabikChalaniFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HalsabikChalanis",
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
                    table.PrimaryKey("PK_HalsabikChalanis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HalsabikChalanis_tblChalan_chalanId",
                        column: x => x.chalanId,
                        principalTable: "tblChalan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HalsabikChalanis_tblSubject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "tblSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HalsabikChalaniPatras",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HalsabikChalaniId = table.Column<int>(nullable: false),
                    fileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalsabikChalaniPatras", x => x.id);
                    table.ForeignKey(
                        name: "FK_HalsabikChalaniPatras_HalsabikChalanis_HalsabikChalaniId",
                        column: x => x.HalsabikChalaniId,
                        principalTable: "HalsabikChalanis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HalsabikChalaniPatras_HalsabikChalaniFiles_fileId",
                        column: x => x.fileId,
                        principalTable: "HalsabikChalaniFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HalsabikChalaniPatras_HalsabikChalaniId",
                table: "HalsabikChalaniPatras",
                column: "HalsabikChalaniId");

            migrationBuilder.CreateIndex(
                name: "IX_HalsabikChalaniPatras_fileId",
                table: "HalsabikChalaniPatras",
                column: "fileId");

            migrationBuilder.CreateIndex(
                name: "IX_HalsabikChalanis_chalanId",
                table: "HalsabikChalanis",
                column: "chalanId");

            migrationBuilder.CreateIndex(
                name: "IX_HalsabikChalanis_subjectId",
                table: "HalsabikChalanis",
                column: "subjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HalsabikChalaniPatras");

            migrationBuilder.DropTable(
                name: "HalsabikChalanis");

            migrationBuilder.DropTable(
                name: "HalsabikChalaniFiles");
        }
    }
}
