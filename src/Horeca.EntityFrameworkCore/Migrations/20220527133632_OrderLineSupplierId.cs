using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horeca.Migrations
{
    public partial class OrderLineSupplierId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "OrderLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_SupplierId",
                table: "OrderLines",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_AbpUsers_SupplierId",
                table: "OrderLines",
                column: "SupplierId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_AbpUsers_SupplierId",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_SupplierId",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "OrderLines");
        }
    }
}
