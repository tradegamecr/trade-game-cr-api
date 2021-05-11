using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeGameCRAPI.Migrations
{
    public partial class entitiesdefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuccessfulDeals",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuccessfulDeals",
                table: "Users");
        }
    }
}
