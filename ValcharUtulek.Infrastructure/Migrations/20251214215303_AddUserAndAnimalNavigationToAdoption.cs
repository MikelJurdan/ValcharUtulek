using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValcharUtulek.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndAnimalNavigationToAdoption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_AnimalId",
                table: "Adoptions",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Animals_AnimalId",
                table: "Adoptions",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Users_UserId",
                table: "Adoptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Animals_AnimalId",
                table: "Adoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Users_UserId",
                table: "Adoptions");

            migrationBuilder.DropIndex(
                name: "IX_Adoptions_AnimalId",
                table: "Adoptions");

            migrationBuilder.DropIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions");
        }
    }
}
