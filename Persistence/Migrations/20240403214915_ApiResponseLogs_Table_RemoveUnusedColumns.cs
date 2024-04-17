using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class ApiResponseLogs_Table_RemoveUnusedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessages",
                table: "ApiResponseLogs");

            migrationBuilder.DropColumn(
                name: "RequestQuery",
                table: "ApiResponseLogs");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "dffb7f4a-91d7-4748-a6c9-ef90d3a8e2ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "420fa08a-d972-4e84-af2d-f07af1d59115");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "595355fc-f918-4542-b579-5d0171c3c629");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "469c030f-30f7-4385-9e1f-414784624db9");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessages",
                table: "ApiResponseLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestQuery",
                table: "ApiResponseLogs",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "d0b0a0af-c8cb-4d1d-b500-ae49072ccd22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "393adc10-c885-4448-99cb-46abb0dff904");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "95e7c947-1c2b-4308-8712-fd425d8cbc27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "a6c8c175-5af0-45fe-9867-a50a9b927782");
        }
    }
}
