using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DataRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChitthiDartaPatras_ChitthiPurjiDarta_purjiDartaId",
                table: "ChitthiDartaPatras");

            migrationBuilder.DropForeignKey(
                name: "FK_ChitthiPurjiDarta_Darta_dartaId",
                table: "ChitthiPurjiDarta");

            migrationBuilder.DropForeignKey(
                name: "FK_ChitthiPurjiDarta_tblSubject_subjectId",
                table: "ChitthiPurjiDarta");

            migrationBuilder.DropTable(
                name: "Darta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChitthiPurjiDarta",
                table: "ChitthiPurjiDarta");

            migrationBuilder.RenameTable(
                name: "ChitthiPurjiDarta",
                newName: "chitthiPurjiDartas");

            migrationBuilder.RenameIndex(
                name: "IX_ChitthiPurjiDarta_subjectId",
                table: "chitthiPurjiDartas",
                newName: "IX_chitthiPurjiDartas_subjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ChitthiPurjiDarta_dartaId",
                table: "chitthiPurjiDartas",
                newName: "IX_chitthiPurjiDartas_dartaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chitthiPurjiDartas",
                table: "chitthiPurjiDartas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Dartas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dartas", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ChitthiDartaPatras_chitthiPurjiDartas_purjiDartaId",
                table: "ChitthiDartaPatras",
                column: "purjiDartaId",
                principalTable: "chitthiPurjiDartas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chitthiPurjiDartas_Dartas_dartaId",
                table: "chitthiPurjiDartas",
                column: "dartaId",
                principalTable: "Dartas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chitthiPurjiDartas_tblSubject_subjectId",
                table: "chitthiPurjiDartas",
                column: "subjectId",
                principalTable: "tblSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChitthiDartaPatras_chitthiPurjiDartas_purjiDartaId",
                table: "ChitthiDartaPatras");

            migrationBuilder.DropForeignKey(
                name: "FK_chitthiPurjiDartas_Dartas_dartaId",
                table: "chitthiPurjiDartas");

            migrationBuilder.DropForeignKey(
                name: "FK_chitthiPurjiDartas_tblSubject_subjectId",
                table: "chitthiPurjiDartas");

            migrationBuilder.DropTable(
                name: "Dartas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chitthiPurjiDartas",
                table: "chitthiPurjiDartas");

            migrationBuilder.RenameTable(
                name: "chitthiPurjiDartas",
                newName: "ChitthiPurjiDarta");

            migrationBuilder.RenameIndex(
                name: "IX_chitthiPurjiDartas_subjectId",
                table: "ChitthiPurjiDarta",
                newName: "IX_ChitthiPurjiDarta_subjectId");

            migrationBuilder.RenameIndex(
                name: "IX_chitthiPurjiDartas_dartaId",
                table: "ChitthiPurjiDarta",
                newName: "IX_ChitthiPurjiDarta_dartaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChitthiPurjiDarta",
                table: "ChitthiPurjiDarta",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Darta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Darta", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ChitthiDartaPatras_ChitthiPurjiDarta_purjiDartaId",
                table: "ChitthiDartaPatras",
                column: "purjiDartaId",
                principalTable: "ChitthiPurjiDarta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChitthiPurjiDarta_Darta_dartaId",
                table: "ChitthiPurjiDarta",
                column: "dartaId",
                principalTable: "Darta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChitthiPurjiDarta_tblSubject_subjectId",
                table: "ChitthiPurjiDarta",
                column: "subjectId",
                principalTable: "tblSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
