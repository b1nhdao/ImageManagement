using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    UploadedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploaderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_documents_uploaders_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "uploaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_documents_UploaderId",
                table: "documents",
                column: "UploaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documents");
        }
    }
}
