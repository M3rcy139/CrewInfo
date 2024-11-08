using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrewInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    PilotId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ResidenceAddress = table.Column<string>(type: "text", nullable: false),
                    MobileNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    PassportNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    PassportIssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassportIssuedBy = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationAddress = table.Column<string>(type: "text", nullable: false),
                    InnNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    InsurancePolicyNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MaritalStatus = table.Column<string>(type: "text", nullable: false),
                    FlightHours = table.Column<int>(type: "integer", nullable: false),
                    Qualification = table.Column<string>(type: "text", nullable: false),
                    LastTrainingLocation = table.Column<string>(type: "text", nullable: false),
                    LastTrainingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CrewNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.PilotId);
                });

            migrationBuilder.CreateTable(
                name: "Stewards",
                columns: table => new
                {
                    StewardId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ResidenceAddress = table.Column<string>(type: "text", nullable: false),
                    MobileNumber = table.Column<int>(type: "integer", maxLength: 15, nullable: false),
                    PassportNumber = table.Column<int>(type: "integer", maxLength: 11, nullable: false),
                    PassportIssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassportIssuedBy = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationAddress = table.Column<string>(type: "text", nullable: false),
                    InnNumber = table.Column<int>(type: "integer", maxLength: 12, nullable: false),
                    InsurancePolicyNumber = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    MaritalStatus = table.Column<string>(type: "text", nullable: false),
                    CrewNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stewards", x => x.StewardId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropTable(
                name: "Stewards");
        }
    }
}
