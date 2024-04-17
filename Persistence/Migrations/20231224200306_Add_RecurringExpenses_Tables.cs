using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Add_RecurringExpenses_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecurringExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: true),
                    RecurringTimeSpanUnit = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_RecurringExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringExpenses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecurringExpenses_BrandCostCategories_BrandCostCategoryId",
                        column: x => x.BrandCostCategoryId,
                        principalTable: "BrandCostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringExpenseAmounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionExecutionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RecurringExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_RecurringExpenseAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringExpenseAmounts_RecurringExpenses_RecurringExpenseId",
                        column: x => x.RecurringExpenseId,
                        principalTable: "RecurringExpenses",
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

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenseAmounts_RecurringExpenseId",
                table: "RecurringExpenseAmounts",
                column: "RecurringExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_BranchId",
                table: "RecurringExpenses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_BrandCostCategoryId",
                table: "RecurringExpenses",
                column: "BrandCostCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringExpenseAmounts");

            migrationBuilder.DropTable(
                name: "RecurringExpenses");

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
                values: new object[] { "3e279d14-b96f-49b5-8f0d-20dcd196b3df", "owner", "owner" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfd037f2-f7fd-4b0c-97ad-d2b00ecfb02b", "branchmanager", "branchmanager" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd5758b6-9965-4b42-96ff-22ad1deb47df", "receptionist", "receptionist" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ed941318-4a5e-4709-9287-85e7dd7db96e", "brandclient", "brandclient" });
        }
    }
}
