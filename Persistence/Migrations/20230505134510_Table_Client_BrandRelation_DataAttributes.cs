using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Table_Client_BrandRelation_DataAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "Clients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BrandId",
                table: "Clients",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Brands_BrandId",
                table: "Clients",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Brands_BrandId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_BrandId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Clients");
        }
    }
}
