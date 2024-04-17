using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Tables_PricingPlans_Adjustments_Photo_Removed_DbColumnsValidations_Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_PricingPlans_PricingPlanId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Photos_LogoId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomService_Visits_VisitId",
                table: "CustomService");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Brands_LogoId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Areas_PricingPlanId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "PricingPlanId",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "VisitId",
                table: "CustomService",
                newName: "SharedAreaVisitId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomService_VisitId",
                table: "CustomService",
                newName: "IX_CustomService_SharedAreaVisitId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PricingPlans",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<Guid>(
                name: "AreaId",
                table: "PricingPlans",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Brands",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(124)",
                oldMaxLength: 124);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(124)",
                oldMaxLength: 124);

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultPricingPlanId",
                table: "Areas",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SharedAreaVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CheckOutStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_SharedAreaVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedAreaVisits_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedAreaVisits_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedAreaVisits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingPlans_AreaId",
                table: "PricingPlans",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_DefaultPricingPlanId",
                table: "Areas",
                column: "DefaultPricingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedAreaVisits_AreaId",
                table: "SharedAreaVisits",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedAreaVisits_BranchId",
                table: "SharedAreaVisits",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedAreaVisits_ClientId",
                table: "SharedAreaVisits",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_PricingPlans_DefaultPricingPlanId",
                table: "Areas",
                column: "DefaultPricingPlanId",
                principalTable: "PricingPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomService_SharedAreaVisits_SharedAreaVisitId",
                table: "CustomService",
                column: "SharedAreaVisitId",
                principalTable: "SharedAreaVisits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PricingPlans_Areas_AreaId",
                table: "PricingPlans",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_PricingPlans_DefaultPricingPlanId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomService_SharedAreaVisits_SharedAreaVisitId",
                table: "CustomService");

            migrationBuilder.DropForeignKey(
                name: "FK_PricingPlans_Areas_AreaId",
                table: "PricingPlans");

            migrationBuilder.DropTable(
                name: "SharedAreaVisits");

            migrationBuilder.DropIndex(
                name: "IX_PricingPlans_AreaId",
                table: "PricingPlans");

            migrationBuilder.DropIndex(
                name: "IX_Areas_DefaultPricingPlanId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "PricingPlans");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "DefaultPricingPlanId",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "SharedAreaVisitId",
                table: "CustomService",
                newName: "VisitId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomService_SharedAreaVisitId",
                table: "CustomService",
                newName: "IX_CustomService_VisitId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PricingPlans",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LogoId",
                table: "Brands",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "character varying(124)",
                maxLength: 124,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "character varying(124)",
                maxLength: 124,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PricingPlanId",
                table: "Areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CheckOutStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_LogoId",
                table: "Brands",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_PricingPlanId",
                table: "Areas",
                column: "PricingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_AreaId",
                table: "Visits",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_BranchId",
                table: "Visits",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ClientId",
                table: "Visits",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_PricingPlans_PricingPlanId",
                table: "Areas",
                column: "PricingPlanId",
                principalTable: "PricingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Photos_LogoId",
                table: "Brands",
                column: "LogoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomService_Visits_VisitId",
                table: "CustomService",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");
        }
    }
}
