using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NipedWebApi.Migrations
{
    /// <inheritdoc />
    public partial class MIGRATIONNAME4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Clients");
        }
    }
}
