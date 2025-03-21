using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class CreteNotifictonTyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationsSent_Companies_CompanyId",
                schema: "starter_core",
                table: "NotificationsSent");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationsSent_Users_UserId",
                schema: "starter_core",
                table: "NotificationsSent");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "starter_core",
                table: "NotificationsSent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                schema: "starter_core",
                table: "NotificationsSent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                schema: "starter_core",
                table: "NotificationsSent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationsSent_NotificationTypeId",
                schema: "starter_core",
                table: "NotificationsSent",
                column: "NotificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationsSent_Companies_CompanyId",
                schema: "starter_core",
                table: "NotificationsSent",
                column: "CompanyId",
                principalSchema: "starter_core",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationsSent_NotificationTypes_NotificationTypeId",
                schema: "starter_core",
                table: "NotificationsSent",
                column: "NotificationTypeId",
                principalSchema: "starter_core",
                principalTable: "NotificationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationsSent_Users_UserId",
                schema: "starter_core",
                table: "NotificationsSent",
                column: "UserId",
                principalSchema: "starter_core",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationsSent_Companies_CompanyId",
                schema: "starter_core",
                table: "NotificationsSent");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationsSent_NotificationTypes_NotificationTypeId",
                schema: "starter_core",
                table: "NotificationsSent");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationsSent_Users_UserId",
                schema: "starter_core",
                table: "NotificationsSent");

            migrationBuilder.DropTable(
                name: "NotificationTypes",
                schema: "starter_core");

            migrationBuilder.DropIndex(
                name: "IX_NotificationsSent_NotificationTypeId",
                schema: "starter_core",
                table: "NotificationsSent");

            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                schema: "starter_core",
                table: "NotificationsSent");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "starter_core",
                table: "NotificationsSent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                schema: "starter_core",
                table: "NotificationsSent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationsSent_Companies_CompanyId",
                schema: "starter_core",
                table: "NotificationsSent",
                column: "CompanyId",
                principalSchema: "starter_core",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationsSent_Users_UserId",
                schema: "starter_core",
                table: "NotificationsSent",
                column: "UserId",
                principalSchema: "starter_core",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
