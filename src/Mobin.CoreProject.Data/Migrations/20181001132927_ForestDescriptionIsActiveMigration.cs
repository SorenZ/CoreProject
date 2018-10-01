using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobin.CoreProject.Data.Migrations
{
    public partial class ForestDescriptionIsActiveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Forest",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Forest",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Forest");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Forest");
        }
    }
}
