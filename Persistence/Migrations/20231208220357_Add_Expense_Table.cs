using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Add_Expense_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionExecutionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    BrandCostCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", unicode: false, nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", unicode: false, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", unicode: false, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_BrandCostCategories_BrandCostCategoryId",
                        column: x => x.BrandCostCategoryId,
                        principalTable: "BrandCostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                values: new object[] { "9533aee7-869b-4d1f-b6ae-a0ef525072b9", "owner", "owner" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b3a9dca-4b42-4037-b724-f6696a65ed2b", "branchmanager", "branchmanager" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dab93136-d1e5-4a35-bdf5-78376ec8ec9e", "receptionist", "receptionist" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f382a3cf-5c71-421c-81c1-47248889dd4d", "brandclient", "brandclient" });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_BranchId",
                table: "Expense",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_BrandCostCategoryId",
                table: "Expense",
                column: "BrandCostCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense");

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
                values: new object[] { "2989b3b0-7ce4-483b-9639-ceed12f7c491", "Owner", "OWNER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8ebf9ca-b944-4a01-9f44-9d9d3366829e", "BranchManager", "BRANCHMANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e5f93b92-c73e-47d9-a565-dd2c42567666", "Receptionist", "RECEPTIONIST" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f7d0f24-5fab-4a02-b895-4747c412f609", "BrandClient", "BRANDCLIENT" });
        }
    }
}
