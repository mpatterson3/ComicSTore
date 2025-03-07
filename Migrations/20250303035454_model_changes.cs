using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicSTore.Migrations
{
    /// <inheritdoc />
    public partial class model_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Comics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Comics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Comics");
        }
    }
}
