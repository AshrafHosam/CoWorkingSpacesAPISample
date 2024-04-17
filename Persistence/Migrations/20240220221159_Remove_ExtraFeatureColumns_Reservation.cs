using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Remove_ExtraFeatureColumns_Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownPayment",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsFullyPaid",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DownPayment",
                table: "Reservations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullyPaid",
                table: "Reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "6e2f9662-7846-4cab-aa85-dd2dcc9307b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "ff2f8ede-1c15-4e07-b1fa-3ce75fa36aea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "600fcff1-87ed-4786-afbc-4616390d69ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "a2a2c600-1f7d-4be1-94ac-b0f3f78310bd");
        }
    }
}
