using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrewInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Stewards",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Stewards",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "InsurancePolicyNumber",
                table: "Stewards",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "InnNumber",
                table: "Stewards",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 12);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PassportNumber",
                table: "Stewards",
                type: "integer",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "Stewards",
                type: "integer",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<int>(
                name: "InsurancePolicyNumber",
                table: "Stewards",
                type: "integer",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "InnNumber",
                table: "Stewards",
                type: "integer",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);
        }
    }
}
