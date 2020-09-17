using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class purjiData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChalanPatras_Files_fileId",
                table: "ChalanPatras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "tblChalanFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblChalanFiles",
                table: "tblChalanFiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Darta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Darta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurjiDartaFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurjiDartaFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChitthiPurjiDarta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DartaNo = table.Column<string>(nullable: true),
                    LetterCount = table.Column<int>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    isTicket = table.Column<bool>(nullable: false),
                    Information = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DartaMiti = table.Column<string>(nullable: true),
                    PatraMiti = table.Column<string>(nullable: true),
                    dartaId = table.Column<int>(nullable: false),
                    subjectId = table.Column<int>(nullable: false),
                    TotalLetterCount = table.Column<int>(nullable: false),
                    extra = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChitthiPurjiDarta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChitthiPurjiDarta_Darta_dartaId",
                        column: x => x.dartaId,
                        principalTable: "Darta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChitthiPurjiDarta_tblSubject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "tblSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChitthiDartaPatras",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purjiDartaId = table.Column<int>(nullable: false),
                    fileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChitthiDartaPatras", x => x.id);
                    table.ForeignKey(
                        name: "FK_ChitthiDartaPatras_PurjiDartaFiles_fileId",
                        column: x => x.fileId,
                        principalTable: "PurjiDartaFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChitthiDartaPatras_ChitthiPurjiDarta_purjiDartaId",
                        column: x => x.purjiDartaId,
                        principalTable: "ChitthiPurjiDarta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiDartaPatras_fileId",
                table: "ChitthiDartaPatras",
                column: "fileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiDartaPatras_purjiDartaId",
                table: "ChitthiDartaPatras",
                column: "purjiDartaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiPurjiDarta_dartaId",
                table: "ChitthiPurjiDarta",
                column: "dartaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiPurjiDarta_subjectId",
                table: "ChitthiPurjiDarta",
                column: "subjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanPatras_tblChalanFiles_fileId",
                table: "ChalanPatras",
                column: "fileId",
                principalTable: "tblChalanFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChalanPatras_tblChalanFiles_fileId",
                table: "ChalanPatras");

            migrationBuilder.DropTable(
                name: "ChitthiDartaPatras");

            migrationBuilder.DropTable(
                name: "PurjiDartaFiles");

            migrationBuilder.DropTable(
                name: "ChitthiPurjiDarta");

            migrationBuilder.DropTable(
                name: "Darta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblChalanFiles",
                table: "tblChalanFiles");

            migrationBuilder.RenameTable(
                name: "tblChalanFiles",
                newName: "Files");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanPatras_Files_fileId",
                table: "ChalanPatras",
                column: "fileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
