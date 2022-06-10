using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class replace_dependent_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderAddressId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_OrderId",
                table: "OrderAddresses",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAddresses_Orders_OrderId",
                table: "OrderAddresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAddresses_Orders_OrderId",
                table: "OrderAddresses");

            migrationBuilder.DropIndex(
                name: "IX_OrderAddresses_OrderId",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderAddresses");

            migrationBuilder.AddColumn<int>(
                name: "OrderAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderAddressId",
                table: "Orders",
                column: "OrderAddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressId",
                table: "Orders",
                column: "OrderAddressId",
                principalTable: "OrderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
