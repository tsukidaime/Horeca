using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horeca.Migrations
{
    public partial class ProductApprovalState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalState",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalState",
                table: "Products");
        }
    }
}
