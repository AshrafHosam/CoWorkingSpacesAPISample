using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class ReservationTable_Adjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Reservations",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "996f38c2-6636-4615-a530-4c4dcb123b8f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "53fedb0b-2c31-438c-b4bb-b7e5702dc375");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "5f07c1d9-3352-4ef8-b389-acef03f3ce7e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "cbd1dc3a-be18-4277-bc75-a9fd81c0c5e4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "b75efdd0-5f85-42bd-99a9-022107f3d56a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "59d32a31-e426-4ef3-80e1-e516fddb871a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "9bbfa4f1-ab0a-4c8d-b239-333a6fcf439f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "4c2712d9-76bc-40e1-9128-0be5e079daf6");
        }
    }
}
