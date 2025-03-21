using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class CreateThirdPartyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThirdPartyTypeList",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyTypeList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThirdPartyTypeList_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThirdPartyTypeList_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyTypeList_CompanyId",
                schema: "starter_core",
                table: "ThirdPartyTypeList",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyTypeList_UserId",
                schema: "starter_core",
                table: "ThirdPartyTypeList",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThirdPartyTypeList",
                schema: "starter_core");
        }
    }
}
