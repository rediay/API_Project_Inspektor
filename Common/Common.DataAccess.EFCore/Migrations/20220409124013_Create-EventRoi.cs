using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class CreateEventRoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventRoiOperationStatuses",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRoiOperationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventRoiOperationTypes",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRoiOperationTypes", x => x.Id);
                });
            
            migrationBuilder.CreateTable(
                name: "EventRois",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedAmount = table.Column<float>(type: "real", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    OperationStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRois", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRois_EventRoiOperationStatuses_OperationStatusId",
                        column: x => x.OperationStatusId,
                        principalSchema: "starter_core",
                        principalTable: "EventRoiOperationStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventRois_EventRoiOperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalSchema: "starter_core",
                        principalTable: "EventRoiOperationTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventRois_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_EventRois_OperationStatusId",
                schema: "starter_core",
                table: "EventRois",
                column: "OperationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRois_OperationTypeId",
                schema: "starter_core",
                table: "EventRois",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRois_UserId",
                schema: "starter_core",
                table: "EventRois",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRois",
                schema: "starter_core");
            
            migrationBuilder.DropTable(
                name: "EventRoiOperationStatuses",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "EventRoiOperationTypes",
                schema: "starter_core");
        }
    }
}
