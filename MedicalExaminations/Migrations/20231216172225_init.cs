using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalExaminations.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalCategories", x => x.Id);
                    table.UniqueConstraint(
                        name: "Unique_AnimalCategories_Name",
                        x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.UniqueConstraint(
                        name: "Unique_Locations_Name",
                        x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAttributes", x => x.Id);
                    table.UniqueConstraint(
                        name: "Unique_OrganizationAttributes_Name",
                        x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.Id);
                    table.UniqueConstraint(
                        name: "Unique_OrganizationTypes_Name",
                        x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "OwnerSigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerSigns", x => x.Id);
                    table.UniqueConstraint(
                        name: "Unique_OwnerSigns_Name",
                        x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PermissionManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionManagers", x => x.Id);
                    table.UniqueConstraint(
                        name: "Unique_PermissionManagers_Name",
                        x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.UniqueConstraint(
                        name: "Unique_UserRoles_Name",
                        x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RegistrationNumber = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    AnimalCategoryId = table.Column<int>(type: "integer", nullable: false),
                    Sex = table.Column<char>(type: "character(1)", nullable: false),
                    BirthYear = table.Column<int>(type: "integer", nullable: false),
                    ChipNumber = table.Column<int>(type: "integer", nullable: false),
                    Nickname = table.Column<string>(type: "varchar(100)", nullable: false),
                    DistinguishingMarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_AnimalCategories_AnimalCategoryId",
                        column: x => x.AnimalCategoryId,
                        principalTable: "AnimalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animals_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.UniqueConstraint(
                        name: "Unique_Animals_RegistrationNumber",
                        x => x.RegistrationNumber);
                    table.UniqueConstraint(
                        name: "Unique_Animals_ChipNumber",
                        x => x.ChipNumber);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    INN = table.Column<long>(type: "numeric(12)", nullable: false),
                    KPP = table.Column<long>(type: "numeric(9)", nullable: false),
                    City = table.Column<string>(type: "varchar(40)", nullable: false),
                    Street = table.Column<string>(type: "varchar(50)", nullable: false),
                    HouseNumber = table.Column<int>(type: "integer", nullable: false),
                    OrganizationTypeId = table.Column<int>(type: "integer", nullable: true),
                    OrganizationAttributeId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationAttributes_OrganizationAttributeId",
                        column: x => x.OrganizationAttributeId,
                        principalTable: "OrganizationAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationTypes_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalTable: "OrganizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.UniqueConstraint(
                        name: "Unique_Organizations_INN",
                        x => x.INN);
                    table.UniqueConstraint(
                        name: "Unique_Organizations_KPP",
                        x => x.KPP);
                });

            migrationBuilder.CreateTable(
                name: "AnimalOwnerSign",
                columns: table => new
                {
                    AnimalsId = table.Column<int>(type: "integer", nullable: false),
                    OwnerSignsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalOwnerSign", x => new { x.AnimalsId, x.OwnerSignsId });
                    table.ForeignKey(
                        name: "FK_AnimalOwnerSign_Animals_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalOwnerSign_OwnerSigns_OwnerSignsId",
                        column: x => x.OwnerSignsId,
                        principalTable: "OwnerSigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AnimalId = table.Column<int>(type: "integer", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalPhotos_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    SigningDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "NOW()"),
                    ValidUntil = table.Column<DateTime>(type: "date", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    ExecutorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Organizations_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Contracts_Organizations_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.UniqueConstraint(
                        name: "Unique_Contracts_Number",
                        x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Patronymic = table.Column<string>(type: "varchar(100)", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    PermissionManagerId = table.Column<int>(type: "integer", nullable: false),
                    WorkplaceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_WorkplaceId",
                        column: x => x.WorkplaceId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_PermissionManagers_PermissionManagerId",
                        column: x => x.PermissionManagerId,
                        principalTable: "PermissionManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.UniqueConstraint(
                        name: "Unique_Users_Login",
                        x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "ContractLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    ContractId = table.Column<int>(type: "integer", nullable: false),
                    ExaminationCost = table.Column<int>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractLocation_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractLocation_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AnimalId = table.Column<int>(type: "integer", nullable: false),
                    BehaviourFeatures = table.Column<string>(type: "varchar(200)", nullable: true),
                    AnimalCondition = table.Column<string>(type: "varchar(200)", nullable: false),
                    BodyTemperature = table.Column<double>(type: "real", nullable: false),
                    SkinCovers = table.Column<string>(type: "varchar(200)", nullable: false),
                    WoolCondition = table.Column<string>(type: "varchar(200)", nullable: false),
                    Injuries = table.Column<string>(type: "varchar(200)", nullable: true),
                    EmergencyHelpRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Diagnosis = table.Column<string>(type: "varchar(200)", nullable: false),
                    ActionsTaken = table.Column<string>(type: "varchar(300)", nullable: true),
                    TreatmentPrescribed = table.Column<string>(type: "varchar(300)", nullable: false),
                    ExaminationDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql : "NOW()"),
                    VeterinarianFullName = table.Column<string>(type: "varchar(200)", nullable: false),
                    VeterinarianPosition = table.Column<string>(type: "varchar(50)", nullable: false),
                    VetClinicId = table.Column<int>(type: "integer", nullable: false),
                    ContractId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_Organizations_VetClinicId",
                        column: x => x.VetClinicId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalOwnerSign_OwnerSignsId",
                table: "AnimalOwnerSign",
                column: "OwnerSignsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalPhotos_AnimalId",
                table: "AnimalPhotos",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalCategoryId",
                table: "Animals",
                column: "AnimalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_LocationId",
                table: "Animals",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractLocation_ContractId",
                table: "ContractLocation",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractLocation_LocationId",
                table: "ContractLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ExecutorId",
                table: "Contracts",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_AnimalId",
                table: "MedicalExaminations",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_ContractId",
                table: "MedicalExaminations",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_VetClinicId",
                table: "MedicalExaminations",
                column: "VetClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_LocationId",
                table: "Organizations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationAttributeId",
                table: "Organizations",
                column: "OrganizationAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationTypeId",
                table: "Organizations",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionManagerId",
                table: "Users",
                column: "PermissionManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkplaceId",
                table: "Users",
                column: "WorkplaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalOwnerSign");

            migrationBuilder.DropTable(
                name: "AnimalPhotos");

            migrationBuilder.DropTable(
                name: "ContractLocation");

            migrationBuilder.DropTable(
                name: "MedicalExaminations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OwnerSigns");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "PermissionManagers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AnimalCategories");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "OrganizationAttributes");

            migrationBuilder.DropTable(
                name: "OrganizationTypes");
        }
    }
}
