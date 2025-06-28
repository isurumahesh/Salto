using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudWorks.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class add_start_end_props_to_schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndUtc",
                schema: "public",
                table: "schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUtc",
                schema: "public",
                table: "schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndUtc",
                schema: "public",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "StartUtc",
                schema: "public",
                table: "schedules");
        }
    }
}
