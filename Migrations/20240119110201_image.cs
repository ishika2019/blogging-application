using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myproject.Migrations
{
    /// <inheritdoc />
    public partial class image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostCategory_Categories_categoriesId",
                table: "BlogPostCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostCategory",
                table: "BlogPostCategory");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostCategory_categoriesId",
                table: "BlogPostCategory");

            migrationBuilder.RenameColumn(
                name: "categoriesId",
                table: "BlogPostCategory",
                newName: "CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostCategory",
                table: "BlogPostCategory",
                columns: new[] { "CategoriesId", "blogPostsId" });

            migrationBuilder.CreateTable(
                name: "blogImages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogImages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostCategory_blogPostsId",
                table: "BlogPostCategory",
                column: "blogPostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostCategory_Categories_CategoriesId",
                table: "BlogPostCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostCategory_Categories_CategoriesId",
                table: "BlogPostCategory");

            migrationBuilder.DropTable(
                name: "blogImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostCategory",
                table: "BlogPostCategory");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostCategory_blogPostsId",
                table: "BlogPostCategory");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "BlogPostCategory",
                newName: "categoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostCategory",
                table: "BlogPostCategory",
                columns: new[] { "blogPostsId", "categoriesId" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostCategory_categoriesId",
                table: "BlogPostCategory",
                column: "categoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostCategory_Categories_categoriesId",
                table: "BlogPostCategory",
                column: "categoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
