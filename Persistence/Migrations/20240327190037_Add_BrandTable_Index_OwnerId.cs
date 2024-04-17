using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Add_BrandTable_Index_OwnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "be5901b1-a8f1-48a9-bd2f-dbfd42c151e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "f931f2ba-2471-4be2-b56c-21ec954963a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "8d334b8b-b795-43d9-8d1f-70f320dc302e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "8066103e-d7f2-4e16-a2bc-7bfbfc8158ba");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_OwnerId",
                table: "Brands",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Brands_OwnerId",
                table: "Brands");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "c62c5e7b-6257-4edf-969b-39149bf56ef5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "c1d0b3aa-ca6d-4c14-a11e-0b12595ecc6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "4bc5413b-4599-4a2a-b2e7-f180b2d618c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "6749ef92-bbaf-455e-8228-445c90538b32");
        }
    }
}
