using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpermCatalog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIsNewAddCustomOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomOrder",
                table: "DairySperms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "DairySperms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CustomOrder",
                table: "BeefSperms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "BeefSperms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomOrder",
                table: "DairySperms");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "DairySperms");

            migrationBuilder.DropColumn(
                name: "CustomOrder",
                table: "BeefSperms");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "BeefSperms");
        }
    }
}
