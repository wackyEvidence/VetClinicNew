using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalExaminations.Migrations
{
    /// <inheritdoc />
    public partial class PermissionManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminations_Contracts_ContractId",
                table: "MedicalExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminations_Organizations_VetClinicId",
                table: "MedicalExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Locations_LocationId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_OrganizationAttributes_OrganizationAttributeId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_OrganizationTypes_OrganizationTypeId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PermissionManagers_PermissionManagerId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PermissionManagers");

            migrationBuilder.DropIndex(
                name: "IX_Users_PermissionManagerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_MedicalExaminations_VetClinicId",
                table: "MedicalExaminations");

            migrationBuilder.DropColumn(
                name: "PermissionManagerId",
                table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "City",
            //    table: "Organizations");

            //migrationBuilder.DropColumn(
            //    name: "MunicipalContractId",
            //    table: "MedicalExaminations");

            //migrationBuilder.DropColumn(
            //    name: "OrganizationId",
            //    table: "MedicalExaminations");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationTypeId",
                table: "Organizations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationAttributeId",
                table: "Organizations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Organizations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "MedicalExaminations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_VetClinicId",
                table: "MedicalExaminations",
                column: "VetClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminations_Contracts_ContractId",
                table: "MedicalExaminations",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminations_Organizations_VetClinicId",
                table: "MedicalExaminations",
                column: "VetClinicId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Locations_LocationId",
                table: "Organizations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_OrganizationAttributes_OrganizationAttributeId",
                table: "Organizations",
                column: "OrganizationAttributeId",
                principalTable: "OrganizationAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_OrganizationTypes_OrganizationTypeId",
                table: "Organizations",
                column: "OrganizationTypeId",
                principalTable: "OrganizationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminations_Contracts_ContractId",
                table: "MedicalExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminations_Organizations_VetClinicId",
                table: "MedicalExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Locations_LocationId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_OrganizationAttributes_OrganizationAttributeId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_OrganizationTypes_OrganizationTypeId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_MedicalExaminations_VetClinicId",
                table: "MedicalExaminations");

            migrationBuilder.AddColumn<int>(
                name: "PermissionManagerId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationTypeId",
                table: "Organizations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationAttributeId",
                table: "Organizations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Organizations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Organizations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "MedicalExaminations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "MunicipalContractId",
                table: "MedicalExaminations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "MedicalExaminations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissionManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionManagers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionManagerId",
                table: "Users",
                column: "PermissionManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_OrganizationId",
                table: "MedicalExaminations",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminations_Contracts_ContractId",
                table: "MedicalExaminations",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminations_Organizations_OrganizationId",
                table: "MedicalExaminations",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Locations_LocationId",
                table: "Organizations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_OrganizationAttributes_OrganizationAttributeId",
                table: "Organizations",
                column: "OrganizationAttributeId",
                principalTable: "OrganizationAttributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_OrganizationTypes_OrganizationTypeId",
                table: "Organizations",
                column: "OrganizationTypeId",
                principalTable: "OrganizationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PermissionManagers_PermissionManagerId",
                table: "Users",
                column: "PermissionManagerId",
                principalTable: "PermissionManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
