using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NipedWebApi.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guidelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guidelines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CholesterolGuidelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuidelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CholesterolGuidelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CholesterolGuidelines_Guidelines_GuidelineId",
                        column: x => x.GuidelineId,
                        principalTable: "Guidelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueGuidelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CholesterolGuidelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Optimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueGuidelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueGuidelines_CholesterolGuidelines_CholesterolGuidelineId",
                        column: x => x.CholesterolGuidelineId,
                        principalTable: "CholesterolGuidelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CholesterolGuidelines_GuidelineId",
                table: "CholesterolGuidelines",
                column: "GuidelineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValueGuidelines_CholesterolGuidelineId",
                table: "ValueGuidelines",
                column: "CholesterolGuidelineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValueGuidelines");

            migrationBuilder.DropTable(
                name: "CholesterolGuidelines");

            migrationBuilder.DropTable(
                name: "Guidelines");
        }
    }
}
