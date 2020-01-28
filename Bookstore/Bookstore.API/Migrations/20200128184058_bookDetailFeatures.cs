using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.API.Migrations
{
    public partial class bookDetailFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherFK_SysID",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers");

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "Publisher");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "SysID");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentSysID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookFK_SysID = table.Column<int>(nullable: false),
                    AppUserFK_SysID = table.Column<string>(nullable: true),
                    CommentText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentSysID);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AppUserFK_SysID",
                        column: x => x.AppUserFK_SysID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Books_BookFK_SysID",
                        column: x => x.BookFK_SysID,
                        principalTable: "Books",
                        principalColumn: "BookSysID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoriteSysID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookFK_SysID = table.Column<int>(nullable: false),
                    AppUserFK_SysID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoriteSysID);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_AppUserFK_SysID",
                        column: x => x.AppUserFK_SysID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorites_Books_BookFK_SysID",
                        column: x => x.BookFK_SysID,
                        principalTable: "Books",
                        principalColumn: "BookSysID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserFK_SysID",
                table: "Comments",
                column: "AppUserFK_SysID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookFK_SysID",
                table: "Comments",
                column: "BookFK_SysID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AppUserFK_SysID",
                table: "Favorites",
                column: "AppUserFK_SysID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_BookFK_SysID",
                table: "Favorites",
                column: "BookFK_SysID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publisher_PublisherFK_SysID",
                table: "Books",
                column: "PublisherFK_SysID",
                principalTable: "Publisher",
                principalColumn: "SysID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publisher_PublisherFK_SysID",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "Publishers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers",
                column: "SysID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherFK_SysID",
                table: "Books",
                column: "PublisherFK_SysID",
                principalTable: "Publishers",
                principalColumn: "SysID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
