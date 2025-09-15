using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickYourTrip.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Packages");
        }
    }
}
