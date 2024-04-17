using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class PricingPlanTable_Columns_Adjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Branches_BranchId",
                table: "Areas");

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

            migrationBuilder.RenameColumn(
                name: "PricingPlanTimeSpan",
                table: "PricingPlans",
                newName: "PricingUnit");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PricingPlans",
                newName: "PricePerUnit");

            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "PricingPlans",
                newName: "MaxUnitsNumber");

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AreaTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("2a024be0-9e86-442a-9437-0cb700e638be"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Room", null, null },
                    { new Guid("2cc71507-07ec-49d5-a8a8-0d1c218b9b39"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Shared", null, null },
                    { new Guid("4f1c928e-585d-4245-8889-18aa6ba24ccb"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Office", null, null },
                    { new Guid("55e8a4ec-f516-48fe-aae1-771c4d7205b3"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Virtual", null, null },
                    { new Guid("7cb8104e-05f0-4ca5-a0c8-160a35f77f9c"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Event", null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Branches_BranchId",
                table: "Areas",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Branches_BranchId",
                table: "Areas");

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("2a024be0-9e86-442a-9437-0cb700e638be"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("2cc71507-07ec-49d5-a8a8-0d1c218b9b39"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("4f1c928e-585d-4245-8889-18aa6ba24ccb"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("55e8a4ec-f516-48fe-aae1-771c4d7205b3"));

            migrationBuilder.DeleteData(
                table: "AreaTypes",
                keyColumn: "Id",
                keyValue: new Guid("7cb8104e-05f0-4ca5-a0c8-160a35f77f9c"));

            migrationBuilder.RenameColumn(
                name: "PricingUnit",
                table: "PricingPlans",
                newName: "PricingPlanTimeSpan");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "PricingPlans",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "MaxUnitsNumber",
                table: "PricingPlans",
                newName: "Hours");

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Areas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Branches_BranchId",
                table: "Areas",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }
    }
}
