using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "images",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "images",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "images",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "images",
                newName: "ImageName");
        }
    }
}
