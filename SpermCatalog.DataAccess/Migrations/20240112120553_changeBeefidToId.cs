using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpermCatalog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeBeefidToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "BeefSperms",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BeefSperms",
                newName: "id");
        }
    }
}
