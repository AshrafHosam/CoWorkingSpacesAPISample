using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class SeedAndEnable_IdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c7b013f0-5201-4317-abd8-c211f91b7330", "6bcffce2-c4ec-40c5-8f1d-e7209ffb849f", "Owner", "OWNER" },
                    { "fab4fac1-c546-41de-aebc-a14da6895711", "28e3087f-fb35-4953-b0c8-418e2b5676d8", "BranchManager", "BRANCHMANAGER" },
                    { "fab4fac1-c546-41de-aebc-a14da6895712", "c0445971-1e08-4cd7-b5d3-40ad9f8c7ed4", "Receptionist", "RECEPTIONIST" },
                    { "fab4fac1-c546-41de-aebc-a14da6895713", "dca84b97-75c9-4086-a15f-fe5d0cdb012f", "BrandClient", "BRANDCLIENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713");
        }
    }
}
