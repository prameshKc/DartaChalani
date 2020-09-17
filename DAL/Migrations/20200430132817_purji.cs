using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class purji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChalanFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChalanFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSubject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChitthiPurjis",
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
                    bsRegDate = table.Column<string>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    chalanId = table.Column<int>(nullable: false),
                    subjectId = table.Column<int>(nullable: false),
                    TotalLetterCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChitthiPurjis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChitthiPurjis_tblChalan_chalanId",
                        column: x => x.chalanId,
                        principalTable: "tblChalan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChitthiPurjis_tblSubject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "tblSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChalanPatras",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purjiId = table.Column<int>(nullable: false),
                    fileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChalanPatras", x => x.id);
                    table.ForeignKey(
                        name: "FK_ChalanPatras_ChalanFiles_fileId",
                        column: x => x.fileId,
                        principalTable: "ChalanFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChalanPatras_ChitthiPurjis_purjiId",
                        column: x => x.purjiId,
                        principalTable: "ChitthiPurjis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChalanPatras_fileId",
                table: "ChalanPatras",
                column: "fileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChalanPatras_purjiId",
                table: "ChalanPatras",
                column: "purjiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiPurjis_chalanId",
                table: "ChitthiPurjis",
                column: "chalanId");

            migrationBuilder.CreateIndex(
                name: "IX_ChitthiPurjis_subjectId",
                table: "ChitthiPurjis",
                column: "subjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChalanPatras");

            migrationBuilder.DropTable(
                name: "ChalanFiles");

            migrationBuilder.DropTable(
                name: "ChitthiPurjis");

            migrationBuilder.DropTable(
                name: "tblSubject");
        }
    }
}
