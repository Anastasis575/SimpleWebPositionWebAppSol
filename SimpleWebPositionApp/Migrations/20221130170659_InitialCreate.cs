using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebPositionApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Codes",
                columns: table => new
                {
                    Barcode = table.Column<string>(type: "TEXT", nullable: false),
                    TopCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codes", x => x.Barcode);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Loginid = table.Column<int>(name: "Login_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoginName = table.Column<string>(name: "Login_Name", type: "TEXT", nullable: false),
                    Pass = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Loginid);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    TopCode = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Balance68 = table.Column<decimal>(type: "TEXT", nullable: false),
                    Position68 = table.Column<string>(type: "TEXT", nullable: false),
                    BalanceCentral = table.Column<decimal>(type: "TEXT", nullable: false),
                    PositionCentral = table.Column<string>(type: "TEXT", nullable: false),
                    Reserved68 = table.Column<decimal>(type: "TEXT", nullable: false),
                    CapacityCentral = table.Column<decimal>(type: "TEXT", nullable: false),
                    Monthly = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionLine = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.TopCode);
                });

            migrationBuilder.CreateTable(
                name: "SearchBar",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchBar", x => x.Code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Codes");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SearchBar");
        }
    }
}
