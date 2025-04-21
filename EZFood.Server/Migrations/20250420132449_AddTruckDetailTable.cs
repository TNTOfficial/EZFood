using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EZFood.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTruckDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TruckDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    TruckOwnerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WhatsappNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BusinessEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsOtherCuisine = table.Column<bool>(type: "bit", nullable: false),
                    CuisineNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BusinessDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BussinessStartYear = table.Column<int>(type: "int", nullable: true),
                    EIN = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsBreakfast = table.Column<bool>(type: "bit", nullable: false),
                    IsLunch = table.Column<bool>(type: "bit", nullable: false),
                    IsDinner = table.Column<bool>(type: "bit", nullable: false),
                    MinimumGuaranteeAmount = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    COI = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    W9 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ServeSafeCertificate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DCHCertificate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    OnboardingStatus = table.Column<int>(type: "int", nullable: false),
                    OnboardingNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckDetails_UserProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TruckDetailCuisineTypes",
                columns: table => new
                {
                    CuisineTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckDetailCuisineTypes", x => new { x.CuisineTypesId, x.TruckDetailsId });
                    table.ForeignKey(
                        name: "FK_TruckDetailCuisineTypes_CuisineTypes_CuisineTypesId",
                        column: x => x.CuisineTypesId,
                        principalTable: "CuisineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TruckDetailCuisineTypes_TruckDetails_TruckDetailsId",
                        column: x => x.TruckDetailsId,
                        principalTable: "TruckDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TruckDetailCuisineTypes_TruckDetailsId",
                table: "TruckDetailCuisineTypes",
                column: "TruckDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckDetails_BusinessEmail",
                table: "TruckDetails",
                column: "BusinessEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TruckDetails_PhoneNumber",
                table: "TruckDetails",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TruckDetails_UserId",
                table: "TruckDetails",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TruckDetailCuisineTypes");

            migrationBuilder.DropTable(
                name: "TruckDetails");
        }
    }
}
