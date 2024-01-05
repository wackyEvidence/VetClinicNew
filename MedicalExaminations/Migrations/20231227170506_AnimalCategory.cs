using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalExaminations.Migrations
{
    /// <inheritdoc />
    public partial class AnimalCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Animals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
