using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class UpdateCompanyTypeList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "starter_core",
                table: "CompanyTypeList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "starter_core",
                table: "CompanyTypeList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "starter_core",
                table: "CompanyTypeList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "starter_core",
                table: "CompanyTypeList");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "starter_core",
                table: "CompanyTypeList");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "starter_core",
                table: "CompanyTypeList");
        }
    }
}
