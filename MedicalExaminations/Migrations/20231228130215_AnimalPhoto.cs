using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalExaminations.Migrations
{
    /// <inheritdoc />
    public partial class AnimalPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "AnimalPhotos");

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoData",
                table: "AnimalPhotos",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoData",
                table: "AnimalPhotos");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "AnimalPhotos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
