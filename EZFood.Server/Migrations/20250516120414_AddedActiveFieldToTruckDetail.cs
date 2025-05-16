using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EZFood.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedActiveFieldToTruckDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TruckDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TruckDetails");
        }
    }
}
