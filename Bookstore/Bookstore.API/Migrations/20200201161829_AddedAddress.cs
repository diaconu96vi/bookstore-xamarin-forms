using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.API.Migrations
{
    public partial class AddedAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressSysID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserFK_SysID = table.Column<string>(nullable: true),
                    AddressTitle = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    FullAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressSysID);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_AppUserFK_SysID",
                        column: x => x.AppUserFK_SysID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppUserFK_SysID",
                table: "Addresses",
                column: "AppUserFK_SysID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
