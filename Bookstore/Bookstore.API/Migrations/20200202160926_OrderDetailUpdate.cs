using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.API.Migrations
{
    public partial class OrderDetailUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BookFK_SysID",
                table: "OrderDetails",
                column: "BookFK_SysID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderFK_SysID",
                table: "OrderDetails",
                column: "OrderFK_SysID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Books_BookFK_SysID",
                table: "OrderDetails",
                column: "BookFK_SysID",
                principalTable: "Books",
                principalColumn: "BookSysID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderFK_SysID",
                table: "OrderDetails",
                column: "OrderFK_SysID",
                principalTable: "Orders",
                principalColumn: "OrderSysID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Books_BookFK_SysID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderFK_SysID",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_BookFK_SysID",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderFK_SysID",
                table: "OrderDetails");
        }
    }
}
