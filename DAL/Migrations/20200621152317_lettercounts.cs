using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class lettercounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "officeName",
                table: "tblSiteSetting",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TotalLetterCount",
                table: "tblFieldRekhankanDarta",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LetterCount",
                table: "tblFieldRekhankanDarta",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TotalLetterCount",
                table: "tblFieldRekhankanChalani",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LetterCount",
                table: "tblFieldRekhankanChalani",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TotalLetterCount",
                table: "HalsabikDartas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MalpotLetterCount",
                table: "HalsabikDartas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TotalLetterCount",
                table: "HalsabikChalanis",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LetterCount",
                table: "HalsabikChalanis",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TotalLetterCount",
                table: "ChitthiPurjis",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LetterCount",
                table: "ChitthiPurjis",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TotalLetterCount",
                table: "chitthiPurjiDartas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LetterCount",
                table: "chitthiPurjiDartas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "officeName",
                table: "tblSiteSetting");

            migrationBuilder.AlterColumn<int>(
                name: "TotalLetterCount",
                table: "tblFieldRekhankanDarta",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LetterCount",
                table: "tblFieldRekhankanDarta",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalLetterCount",
                table: "tblFieldRekhankanChalani",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LetterCount",
                table: "tblFieldRekhankanChalani",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalLetterCount",
                table: "HalsabikDartas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MalpotLetterCount",
                table: "HalsabikDartas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalLetterCount",
                table: "HalsabikChalanis",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LetterCount",
                table: "HalsabikChalanis",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalLetterCount",
                table: "ChitthiPurjis",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LetterCount",
                table: "ChitthiPurjis",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalLetterCount",
                table: "chitthiPurjiDartas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LetterCount",
                table: "chitthiPurjiDartas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
