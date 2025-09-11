using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "uploaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uploaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    Size_Height = table.Column<int>(type: "integer", nullable: false),
                    Size_Width = table.Column<int>(type: "integer", nullable: false),
                    UploadedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploaderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_images_uploaders_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "uploaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_UploaderId",
                table: "images",
                column: "UploaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "uploaders");
        }
    }
}
