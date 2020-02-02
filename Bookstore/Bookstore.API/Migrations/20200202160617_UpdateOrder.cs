using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.API.Migrations
{
    public partial class UpdateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressFK_SysID",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardFK_Sys",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressFK_SysID",
                table: "Orders",
                column: "AddressFK_SysID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CardFK_Sys",
                table: "Orders",
                column: "CardFK_Sys");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressFK_SysID",
                table: "Orders",
                column: "AddressFK_SysID",
                principalTable: "Addresses",
                principalColumn: "AddressSysID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cards_CardFK_Sys",
                table: "Orders",
                column: "CardFK_Sys",
                principalTable: "Cards",
                principalColumn: "SysID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressFK_SysID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cards_CardFK_Sys",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AddressFK_SysID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CardFK_Sys",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressFK_SysID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardFK_Sys",
                table: "Orders");
        }
    }
}
