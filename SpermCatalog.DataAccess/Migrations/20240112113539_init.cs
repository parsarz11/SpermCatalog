using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpermCatalog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeefSperms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BREED = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCE = table.Column<double>(type: "float", nullable: false),
                    CR = table.Column<double>(type: "float", nullable: false),
                    DM = table.Column<double>(type: "float", nullable: false),
                    PCAR = table.Column<double>(type: "float", nullable: false),
                    RDT = table.Column<double>(type: "float", nullable: false),
                    CONF = table.Column<double>(type: "float", nullable: false),
                    COUL = table.Column<double>(type: "float", nullable: false),
                    GRAS = table.Column<double>(type: "float", nullable: false),
                    IAB = table.Column<double>(type: "float", nullable: false),
                    ICRC = table.Column<double>(type: "float", nullable: false),
                    SIRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MGS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeefSperms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DairySperms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAAB_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICC = table.Column<double>(type: "float", nullable: false),
                    LNM = table.Column<double>(type: "float", nullable: false),
                    FM = table.Column<double>(type: "float", nullable: false),
                    MILK = table.Column<double>(type: "float", nullable: false),
                    FAT = table.Column<double>(type: "float", nullable: false),
                    PRO = table.Column<double>(type: "float", nullable: false),
                    SCE = table.Column<double>(type: "float", nullable: false),
                    PL = table.Column<double>(type: "float", nullable: false),
                    DPR = table.Column<double>(type: "float", nullable: false),
                    PTAT = table.Column<double>(type: "float", nullable: false),
                    UDC = table.Column<double>(type: "float", nullable: false),
                    FLC = table.Column<double>(type: "float", nullable: false),
                    FS = table.Column<double>(type: "float", nullable: false),
                    FI = table.Column<double>(type: "float", nullable: false),
                    TPI = table.Column<double>(type: "float", nullable: false),
                    SIRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MGS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DairySperms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeefSperms");

            migrationBuilder.DropTable(
                name: "DairySperms");
        }
    }
}
