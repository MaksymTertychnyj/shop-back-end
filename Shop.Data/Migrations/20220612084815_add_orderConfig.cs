using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class add_orderConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAddresses_Orders_OrderId",
                table: "OrderAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderAddresses",
                table: "OrderAddresses");

            migrationBuilder.RenameTable(
                name: "OrderAddresses",
                newName: "OrderAddress");

            migrationBuilder.RenameIndex(
                name: "IX_OrderAddresses_OrderId",
                table: "OrderAddress",
                newName: "IX_OrderAddress_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderAddress",
                table: "OrderAddress",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Id",
                table: "Orders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_Id",
                table: "OrderProducts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddress_Id",
                table: "OrderAddress",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAddress_Orders_OrderId",
                table: "OrderAddress",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAddress_Orders_OrderId",
                table: "OrderAddress");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_Id",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderAddress",
                table: "OrderAddress");

            migrationBuilder.DropIndex(
                name: "IX_OrderAddress_Id",
                table: "OrderAddress");

            migrationBuilder.RenameTable(
                name: "OrderAddress",
                newName: "OrderAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_OrderAddress_OrderId",
                table: "OrderAddresses",
                newName: "IX_OrderAddresses_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderAddresses",
                table: "OrderAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAddresses_Orders_OrderId",
                table: "OrderAddresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
