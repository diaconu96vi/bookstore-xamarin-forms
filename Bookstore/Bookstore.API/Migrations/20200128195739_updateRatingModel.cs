using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.API.Migrations
{
    public partial class updateRatingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publisher_PublisherFK_SysID",
                table: "Books");

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

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingSysID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookFK_SysID = table.Column<int>(nullable: false),
                    AppUserFK_SysID = table.Column<string>(nullable: true),
                    RatingGrade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingSysID);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_AppUserFK_SysID",
                        column: x => x.AppUserFK_SysID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Books_BookFK_SysID",
                        column: x => x.BookFK_SysID,
                        principalTable: "Books",
                        principalColumn: "BookSysID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AppUserFK_SysID",
                table: "Ratings",
                column: "AppUserFK_SysID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookFK_SysID",
                table: "Ratings",
                column: "BookFK_SysID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherFK_SysID",
                table: "Books",
                column: "PublisherFK_SysID",
                principalTable: "Publishers",
                principalColumn: "SysID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherFK_SysID",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Ratings");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publisher_PublisherFK_SysID",
                table: "Books",
                column: "PublisherFK_SysID",
                principalTable: "Publisher",
                principalColumn: "SysID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
