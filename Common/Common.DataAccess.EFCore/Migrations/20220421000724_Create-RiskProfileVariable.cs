using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class CreateRiskProfileVariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskProfileVariables",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_riskProfileVariables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryVariables",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    RiskProfileVariableId = table.Column<int>(type: "int", nullable: false),
                    PersonTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryVariables_riskProfileVariables_RiskProfileVariableId",
                        column: x => x.RiskProfileVariableId,
                        principalSchema: "starter_core",
                        principalTable: "riskProfileVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_CategoryVariables_RiskProfileVariableId",
                schema: "starter_core",
                table: "CategoryVariables",
                column: "RiskProfileVariableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryVariables",
                schema: "starter_core");
            
            migrationBuilder.DropTable(
                name: "RiskProfileVariables",
                schema: "starter_core");
        }
    }
}
