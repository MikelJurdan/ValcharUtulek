using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValcharUtulek.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Animals",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Animals");
        }
    }
}
