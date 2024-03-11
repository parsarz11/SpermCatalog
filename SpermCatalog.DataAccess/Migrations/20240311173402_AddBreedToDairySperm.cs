using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpermCatalog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBreedToDairySperm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "DairySperms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breed",
                table: "DairySperms");
        }
    }
}
