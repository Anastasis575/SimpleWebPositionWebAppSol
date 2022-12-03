using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebPositionApp.Migrations
{
    /// <inheritdoc />
    public partial class AppendLoggin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "mode",
                table: "Login",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mode",
                table: "Login");
        }
    }
}
