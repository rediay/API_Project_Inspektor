using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class UpdateCompanyTypeList2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Consultar",
                schema: "starter_core",
                table: "CompanyTypeList",
                newName: "Search");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Search",
                schema: "starter_core",
                table: "CompanyTypeList",
                newName: "Consultar");
        }
    }
}
