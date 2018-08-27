using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scraping.Web.Entities.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EconomicCalenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Time = table.Column<string>(maxLength: 50, nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Event = table.Column<string>(nullable: true),
                    Vol = table.Column<string>(nullable: true),
                    Actual = table.Column<string>(nullable: true),
                    Consensus = table.Column<string>(nullable: true),
                    Previous = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicCalenders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EconomicCalenders");
        }
    }
}
