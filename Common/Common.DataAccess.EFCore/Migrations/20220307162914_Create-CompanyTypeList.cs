using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class CreateCompanyTypeList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId",
                schema: "starter_core",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                schema: "starter_core",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyTypeList",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consultar = table.Column<bool>(type: "bit", nullable: false),
                    ListTypeId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypeList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTypeList_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyTypeList_ListTypes_ListTypeId",
                        column: x => x.ListTypeId,
                        principalSchema: "starter_core",
                        principalTable: "ListTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyTypeList_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypeList_CompanyId",
                schema: "starter_core",
                table: "CompanyTypeList",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypeList_ListTypeId",
                schema: "starter_core",
                table: "CompanyTypeList",
                column: "ListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypeList_UserId",
                schema: "starter_core",
                table: "CompanyTypeList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId",
                schema: "starter_core",
                table: "Users",
                column: "CompanyId",
                principalSchema: "starter_core",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId",
                schema: "starter_core",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CompanyTypeList",
                schema: "starter_core");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                schema: "starter_core",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId",
                schema: "starter_core",
                table: "Users",
                column: "CompanyId",
                principalSchema: "starter_core",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
