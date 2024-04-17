using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class PricingPlansSimplified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_PricingPlans_DefaultPricingPlanId",
                table: "Areas");

            migrationBuilder.DropTable(
                name: "PricingPlans");

            migrationBuilder.Operations.Add(new SqlOperation
            {
                Sql = "DELETE FROM public.\"CustomService\""
            });

            migrationBuilder.Operations.Add(new SqlOperation
            {
                Sql = "DELETE FROM public.\"SharedAreaVisits\""
            });

            migrationBuilder.Operations.Add(new SqlOperation
            {
                Sql = "DELETE FROM public.\"Areas\""
            });

            migrationBuilder.RenameColumn(
                name: "DefaultPricingPlanId",
                table: "Areas",
                newName: "SharedAreaPricingPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_DefaultPricingPlanId",
                table: "Areas",
                newName: "IX_Areas_SharedAreaPricingPlanId");

            migrationBuilder.AddColumn<Guid>(
                name: "BookableAreaPricingPlanId",
                table: "Areas",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookableAreaPricingPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "numeric", nullable: true),
                    PricePerDay = table.Column<decimal>(type: "numeric", nullable: true),
                    PricePerMonth = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookableAreaPricingPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SharedPricingPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "numeric", nullable: false),
                    IsFullDayApplicable = table.Column<bool>(type: "boolean", nullable: false),
                    FullDayHours = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedPricingPlans", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "8e4859e3-34ac-421a-9e4d-e8625a861262");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "12fc6053-3c42-4f30-bf1c-8dec5b66a2da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "a6661f4b-28e5-424f-bdc7-2d5ec9579875");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "9e37d320-154a-4b2e-8cb1-56f1e957d278");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_BookableAreaPricingPlanId",
                table: "Areas",
                column: "BookableAreaPricingPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_BookableAreaPricingPlans_BookableAreaPricingPlanId",
                table: "Areas",
                column: "BookableAreaPricingPlanId",
                principalTable: "BookableAreaPricingPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_SharedPricingPlans_SharedAreaPricingPlanId",
                table: "Areas",
                column: "SharedAreaPricingPlanId",
                principalTable: "SharedPricingPlans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_BookableAreaPricingPlans_BookableAreaPricingPlanId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_SharedPricingPlans_SharedAreaPricingPlanId",
                table: "Areas");

            migrationBuilder.DropTable(
                name: "BookableAreaPricingPlans");

            migrationBuilder.DropTable(
                name: "SharedPricingPlans");

            migrationBuilder.DropIndex(
                name: "IX_Areas_BookableAreaPricingPlanId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "BookableAreaPricingPlanId",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "SharedAreaPricingPlanId",
                table: "Areas",
                newName: "DefaultPricingPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_SharedAreaPricingPlanId",
                table: "Areas",
                newName: "IX_Areas_DefaultPricingPlanId");

            migrationBuilder.CreateTable(
                name: "PricingPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: true),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    MaxUnitsNumber = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "numeric", nullable: false),
                    PricingUnit = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingPlans_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PricingPlans_AreaTypes_AreaTypeId",
                        column: x => x.AreaTypeId,
                        principalTable: "AreaTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PricingPlans_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "0edd2b70-9864-4431-b74c-be1e6cc3b2f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "ConcurrencyStamp",
                value: "3b421327-cf7d-48df-80b3-f06d56ff25ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895712",
                column: "ConcurrencyStamp",
                value: "3934bca1-2abb-46c0-90e8-8aa03f8787b6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895713",
                column: "ConcurrencyStamp",
                value: "2ac495f1-c6d0-43ed-b871-92a89213ea1a");

            migrationBuilder.CreateIndex(
                name: "IX_PricingPlans_AreaId",
                table: "PricingPlans",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingPlans_AreaTypeId",
                table: "PricingPlans",
                column: "AreaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingPlans_BrandId",
                table: "PricingPlans",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_PricingPlans_DefaultPricingPlanId",
                table: "Areas",
                column: "DefaultPricingPlanId",
                principalTable: "PricingPlans",
                principalColumn: "Id");
        }
    }
}
