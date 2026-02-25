using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octet.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    DeltaDate = table.Column<double>(type: "double precision", nullable: false),
                    MinStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AvgExecutionTime = table.Column<double>(type: "double precision", nullable: false),
                    AvgRate = table.Column<double>(type: "double precision", nullable: false),
                    MedianRate = table.Column<double>(type: "double precision", nullable: false),
                    MaxRate = table.Column<double>(type: "double precision", nullable: false),
                    MinRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "file_values",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutionTime = table.Column<double>(type: "double precision", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_file_values_results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_file_values_ResultId",
                table: "file_values",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_results_FileName",
                table: "results",
                column: "FileName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_values");

            migrationBuilder.DropTable(
                name: "results");
        }
    }
}
