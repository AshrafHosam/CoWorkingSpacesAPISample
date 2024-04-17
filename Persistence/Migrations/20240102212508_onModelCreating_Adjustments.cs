using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class onModelCreating_Adjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("2a024be0-9e86-442a-9437-0cb700e638be"),
                column: "Name",
                value: "Room");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("2cc71507-07ec-49d5-a8a8-0d1c218b9b39"),
                column: "Name",
                value: "Shared");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("4f1c928e-585d-4245-8889-18aa6ba24ccb"),
                column: "Name",
                value: "Office");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("55e8a4ec-f516-48fe-aae1-771c4d7205b3"),
                column: "Name",
                value: "Virtual");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("7cb8104e-05f0-4ca5-a0c8-160a35f77f9c"),
                column: "Name",
                value: "Event");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0edd2b70-9864-4431-b74c-be1e6cc3b2f5", "Owner", "OWNER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b421327-cf7d-48df-80b3-f06d56ff25ea", "BranchManager", "BRANCHMANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3934bca1-2abb-46c0-90e8-8aa03f8787b6", "Receptionist", "RECEPTIONIST" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ac495f1-c6d0-43ed-b871-92a89213ea1a", "BrandClient", "BRANDCLIENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("2a024be0-9e86-442a-9437-0cb700e638be"),
                column: "Name",
                value: "room");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("2cc71507-07ec-49d5-a8a8-0d1c218b9b39"),
                column: "Name",
                value: "shared");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("4f1c928e-585d-4245-8889-18aa6ba24ccb"),
                column: "Name",
                value: "office");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("55e8a4ec-f516-48fe-aae1-771c4d7205b3"),
                column: "Name",
                value: "virtual");

            migrationBuilder.UpdateData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("7cb8104e-05f0-4ca5-a0c8-160a35f77f9c"),
                column: "Name",
                value: "event");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1fe3745-ccea-4426-add1-7b0535fd21e5", "owner", "owner" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dd39d67f-e24e-4824-9648-e3419a8db1cf", "branchmanager", "branchmanager" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c9ae27f-4f04-4b1e-a39a-8e6065008190", "receptionist", "receptionist" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f85b53a1-0bca-4a44-8ec0-0b5fc2a651bc", "brandclient", "brandclient" });
        }
    }
}
