using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horeca.Migrations
{
    public partial class OrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Products_ProductId",
                table: "OrderLines");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderLines",
                newName: "ProductBidId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLines_ProductId",
                table: "OrderLines",
                newName: "IX_OrderLines_ProductBidId");

            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Bids_ProductBidId",
                table: "OrderLines",
                column: "ProductBidId",
                principalTable: "Bids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Bids_ProductBidId",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductBidId",
                table: "OrderLines",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLines_ProductBidId",
                table: "OrderLines",
                newName: "IX_OrderLines_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Products_ProductId",
                table: "OrderLines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
