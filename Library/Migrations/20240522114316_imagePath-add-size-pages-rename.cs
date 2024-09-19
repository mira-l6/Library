using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class imagePathaddsizepagesrename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Books",
                newName: "Pages");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Pages",
                table: "Books",
                newName: "Size");
        }
    }
}
