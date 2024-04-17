using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Add_Table_CustomServiceCategory_Relation_CustomService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomServiceCategoryId",
                table: "CustomService",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomServiceCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_CustomServiceCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomServiceCategories_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomService_CustomServiceCategoryId",
                table: "CustomService",
                column: "CustomServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomServiceCategories_BrandId",
                table: "CustomServiceCategories",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomService_CustomServiceCategories_CustomServiceCategory~",
                table: "CustomService",
                column: "CustomServiceCategoryId",
                principalTable: "CustomServiceCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomService_CustomServiceCategories_CustomServiceCategory~",
                table: "CustomService");

            migrationBuilder.DropTable(
                name: "CustomServiceCategories");

            migrationBuilder.DropIndex(
                name: "IX_CustomService_CustomServiceCategoryId",
                table: "CustomService");

            migrationBuilder.DropColumn(
                name: "CustomServiceCategoryId",
                table: "CustomService");
        }
    }
}
