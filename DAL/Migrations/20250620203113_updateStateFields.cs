using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateStateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentNumber",
                table: "document");

            migrationBuilder.DropColumn(
                name: "IssuingAgency",
                table: "document");

            migrationBuilder.RenameColumn(
                name: "Abreviation",
                table: "state",
                newName: "Abbreviation");

            migrationBuilder.AlterColumn<string>(
                name: "Batch",
                table: "vaccine",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Abbreviation",
                table: "state",
                newName: "Abreviation");

            migrationBuilder.AlterColumn<string>(
                name: "Batch",
                table: "vaccine",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "DocumentNumber",
                table: "document",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuingAgency",
                table: "document",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
