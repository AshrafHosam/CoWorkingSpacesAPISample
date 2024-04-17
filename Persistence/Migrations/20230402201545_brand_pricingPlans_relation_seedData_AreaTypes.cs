using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class brand_pricingPlans_relation_seedData_AreaTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "PricingPlans",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AreaTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0089483d-d9c6-4f66-b57f-e7a7f4ee4e99"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Office", null, null },
                    { new Guid("72ff82c1-21c1-4e81-8362-ba4db854f7f0"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Virtual", null, null },
                    { new Guid("753d36d1-2776-454b-944b-4b5619493c20"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Shared", null, null },
                    { new Guid("ecfcd31c-4b6e-4671-998f-3d8fc278bdb4"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Event", null, null },
                    { new Guid("f113b30e-ecb9-47e3-b050-8bbd7d528dfb"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Room", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingPlans_BrandId",
                table: "PricingPlans",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricingPlans_Brands_BrandId",
                table: "PricingPlans",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricingPlans_Brands_BrandId",
                table: "PricingPlans");

            migrationBuilder.DropIndex(
                name: "IX_PricingPlans_BrandId",
                table: "PricingPlans");

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("0089483d-d9c6-4f66-b57f-e7a7f4ee4e99"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("72ff82c1-21c1-4e81-8362-ba4db854f7f0"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("753d36d1-2776-454b-944b-4b5619493c20"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("ecfcd31c-4b6e-4671-998f-3d8fc278bdb4"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("f113b30e-ecb9-47e3-b050-8bbd7d528dfb"));

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "PricingPlans");
        }
    }
}
