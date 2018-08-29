using Microsoft.EntityFrameworkCore.Migrations;

namespace Scraping.Web.Entities.Migrations
{
    public partial class updatefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "EconomicCalenders",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "EconomicCalenders",
                newName: "Month");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "EconomicCalenders",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "EconomicCalenders",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateText",
                table: "EconomicCalenders",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "EconomicCalenders",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "EconomicCalenders");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "EconomicCalenders");

            migrationBuilder.DropColumn(
                name: "DateText",
                table: "EconomicCalenders");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "EconomicCalenders");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "EconomicCalenders",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "EconomicCalenders",
                newName: "Country");
        }
    }
}
