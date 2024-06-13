using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AquaTrack.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    SensorDataId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SensorType = table.Column<int>(type: "integer", nullable: false),
                    SensorValue = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SensorStatus = table.Column<string>(type: "text", nullable: true),
                    SensorIdentificator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.SensorDataId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Aquariums",
                columns: table => new
                {
                    AquariumId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AquariumType = table.Column<string>(type: "text", nullable: false),
                    Acidity = table.Column<float>(type: "real", nullable: true),
                    WaterLevel = table.Column<float>(type: "real", nullable: true),
                    Temperature = table.Column<float>(type: "real", nullable: true),
                    Lighting = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquariums", x => x.AquariumId);
                    table.ForeignKey(
                        name: "FK_Aquariums_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedingSchedules",
                columns: table => new
                {
                    FeedingScheduleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AquariumId = table.Column<int>(type: "integer", nullable: false),
                    FeedTime = table.Column<float>(type: "real", nullable: false),
                    FeedAmount = table.Column<float>(type: "real", nullable: true),
                    FeedType = table.Column<string>(type: "text", nullable: true),
                    RepeatInterval = table.Column<float>(type: "real", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingSchedules", x => x.FeedingScheduleId);
                    table.ForeignKey(
                        name: "FK_FeedingSchedules_Aquariums_AquariumId",
                        column: x => x.AquariumId,
                        principalTable: "Aquariums",
                        principalColumn: "AquariumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inhabitants",
                columns: table => new
                {
                    InhabitantId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AquariumId = table.Column<int>(type: "integer", nullable: false),
                    Species = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserNotes = table.Column<string>(type: "text", nullable: true),
                    Behavior = table.Column<string>(type: "text", nullable: true),
                    Condition = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inhabitants", x => x.InhabitantId);
                    table.ForeignKey(
                        name: "FK_Inhabitants_Aquariums_AquariumId",
                        column: x => x.AquariumId,
                        principalTable: "Aquariums",
                        principalColumn: "AquariumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchReports",
                columns: table => new
                {
                    ResearchReportId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnalysisReportId = table.Column<int>(type: "integer", nullable: false),
                    AquariumId = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchReports", x => x.ResearchReportId);
                    table.ForeignKey(
                        name: "FK_ResearchReports_Aquariums_AquariumId",
                        column: x => x.AquariumId,
                        principalTable: "Aquariums",
                        principalColumn: "AquariumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisReports",
                columns: table => new
                {
                    AnalysisReportId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResearchReportId = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IdentifiedTrends = table.Column<string>(type: "text", nullable: false),
                    Recommendations = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisReports", x => x.AnalysisReportId);
                    table.ForeignKey(
                        name: "FK_AnalysisReports_ResearchReports_ResearchReportId",
                        column: x => x.ResearchReportId,
                        principalTable: "ResearchReports",
                        principalColumn: "ResearchReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchReportSensorData",
                columns: table => new
                {
                    ResearchReportsResearchReportId = table.Column<int>(type: "integer", nullable: false),
                    SensorDataId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchReportSensorData", x => new { x.ResearchReportsResearchReportId, x.SensorDataId });
                    table.ForeignKey(
                        name: "FK_ResearchReportSensorData_ResearchReports_ResearchReportsRes~",
                        column: x => x.ResearchReportsResearchReportId,
                        principalTable: "ResearchReports",
                        principalColumn: "ResearchReportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearchReportSensorData_SensorData_SensorDataId",
                        column: x => x.SensorDataId,
                        principalTable: "SensorData",
                        principalColumn: "SensorDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisReports_ResearchReportId",
                table: "AnalysisReports",
                column: "ResearchReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aquariums_UserId",
                table: "Aquariums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingSchedules_AquariumId",
                table: "FeedingSchedules",
                column: "AquariumId");

            migrationBuilder.CreateIndex(
                name: "IX_Inhabitants_AquariumId",
                table: "Inhabitants",
                column: "AquariumId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchReports_AquariumId",
                table: "ResearchReports",
                column: "AquariumId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchReportSensorData_SensorDataId",
                table: "ResearchReportSensorData",
                column: "SensorDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisReports");

            migrationBuilder.DropTable(
                name: "FeedingSchedules");

            migrationBuilder.DropTable(
                name: "Inhabitants");

            migrationBuilder.DropTable(
                name: "ResearchReportSensorData");

            migrationBuilder.DropTable(
                name: "ResearchReports");

            migrationBuilder.DropTable(
                name: "SensorData");

            migrationBuilder.DropTable(
                name: "Aquariums");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
