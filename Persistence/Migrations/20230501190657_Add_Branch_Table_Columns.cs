using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Add_Branch_Table_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationUrl",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Branches",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "LocationUrl",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Branches");
        }
    }
}
