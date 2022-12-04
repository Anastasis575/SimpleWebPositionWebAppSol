using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebPositionApp.Migrations
{
    /// <inheritdoc />
    public partial class Added64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "Products64",
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
                    table.PrimaryKey("PK_Products64", x => x.TopCode);
                });

            migrationBuilder.CreateTable(
                name: "Products68",
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
                    table.PrimaryKey("PK_Products68", x => x.TopCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products64");

            migrationBuilder.DropTable(
                name: "Products68");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    TopCode = table.Column<string>(type: "TEXT", nullable: false),
                    Balance68 = table.Column<decimal>(type: "TEXT", nullable: false),
                    BalanceCentral = table.Column<decimal>(type: "TEXT", nullable: false),
                    CapacityCentral = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Monthly = table.Column<decimal>(type: "TEXT", nullable: false),
                    Position68 = table.Column<string>(type: "TEXT", nullable: false),
                    PositionCentral = table.Column<string>(type: "TEXT", nullable: false),
                    Reserved68 = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionLine = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.TopCode);
                });
        }
    }
}
