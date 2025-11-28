using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTagTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersianTitle",
                table: "Tags",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "EngilshTitle",
                table: "Tags",
                newName: "Slug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tags",
                newName: "PersianTitle");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Tags",
                newName: "EngilshTitle");
        }
    }
}
