using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudWorks.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class add_accessevent_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "access_events",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_id = table.Column<Guid>(type: "uuid", nullable: false),
                    access_point_id = table.Column<Guid>(type: "uuid", nullable: false),
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    success = table.Column<bool>(type: "boolean", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_access_events_access_points_access_point_id",
                        column: x => x.access_point_id,
                        principalSchema: "public",
                        principalTable: "access_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_access_events_profiles_profile_id",
                        column: x => x.profile_id,
                        principalSchema: "public",
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_access_events_sites_site_id",
                        column: x => x.site_id,
                        principalSchema: "public",
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_events_access_point_id",
                schema: "public",
                table: "access_events",
                column: "access_point_id");

            migrationBuilder.CreateIndex(
                name: "IX_access_events_profile_id",
                schema: "public",
                table: "access_events",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_access_events_site_id",
                schema: "public",
                table: "access_events",
                column: "site_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_events",
                schema: "public");
        }
    }
}