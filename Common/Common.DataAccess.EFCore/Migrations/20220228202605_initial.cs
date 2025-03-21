using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "starter_core");

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListGroups",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Blank = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewStatuses",
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
                    table.PrimaryKey("PK_NewStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewTypes",
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
                    table.PrimaryKey("PK_NewTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periodicities",
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
                    table.PrimaryKey("PK_Periodicities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
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
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubMenus",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Blank = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "starter_core",
                        principalTable: "Menus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ListTypes",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodicityId = table.Column<int>(type: "int", nullable: true),
                    ListGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListTypes_ListGroups_ListGroupId",
                        column: x => x.ListGroupId,
                        principalSchema: "starter_core",
                        principalTable: "ListGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListTypes_Periodicities_PeriodicityId",
                        column: x => x.PeriodicityId,
                        principalSchema: "starter_core",
                        principalTable: "Periodicities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AutoRenewal = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsCondition = table.Column<bool>(type: "bit", nullable: false),
                    HasResetPassword = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lists",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KindPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoreInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ListTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lists_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "starter_core",
                        principalTable: "DocumentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lists_ListTypes_ListTypeId",
                        column: x => x.ListTypeId,
                        principalSchema: "starter_core",
                        principalTable: "ListTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lists_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NewCategories",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewCategories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendMailPriority1 = table.Column<bool>(type: "bit", nullable: false),
                    MailPriority1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendMailPriority2 = table.Column<bool>(type: "bit", nullable: false),
                    MailPriority2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendMailPriority3 = table.Column<bool>(type: "bit", nullable: false),
                    MailPriority3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendMailPriority4 = table.Column<bool>(type: "bit", nullable: false),
                    MailPriority4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendMailCoincidences = table.Column<bool>(type: "bit", nullable: false),
                    MailCoincidences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendMailAdditionalServices = table.Column<bool>(type: "bit", nullable: false),
                    MailAdditionalServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OwnListTypes",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnListTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnListTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OwnListTypes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QtyQueries = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Queries",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQueryCompany = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Queries_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ThemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Users_Id",
                        column: x => x.Id,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPhotos",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPhotos_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "starter_core",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "starter_core",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    NewCategoryId = table.Column<int>(type: "int", nullable: true),
                    NewTypeId = table.Column<int>(type: "int", nullable: true),
                    NewStatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_NewCategories_NewCategoryId",
                        column: x => x.NewCategoryId,
                        principalSchema: "starter_core",
                        principalTable: "NewCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_NewStatuses_NewStatusId",
                        column: x => x.NewStatusId,
                        principalSchema: "starter_core",
                        principalTable: "NewStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_NewTypes_NewTypeId",
                        column: x => x.NewTypeId,
                        principalSchema: "starter_core",
                        principalTable: "NewTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OwnLists",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeIdentification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoreInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    OwnListTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnLists_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OwnLists_OwnListTypes_OwnListTypeId",
                        column: x => x.OwnListTypeId,
                        principalSchema: "starter_core",
                        principalTable: "OwnListTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OwnLists_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QueryDetails",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueryDetails_Queries_QueryId",
                        column: x => x.QueryId,
                        principalSchema: "starter_core",
                        principalTable: "Queries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BulkQueryServicesAdditional",
                schema: "starter_core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    attorneyService = table.Column<bool>(type: "bit", nullable: false),
                    judicialBranchService = table.Column<bool>(type: "bit", nullable: false),
                    jempsJudicialBranchService = table.Column<bool>(type: "bit", nullable: false),
                    ConsultingStatus = table.Column<bool>(type: "bit", nullable: false),
                    CurrentConsulting = table.Column<int>(type: "int", nullable: true),
                    TotalConsulting = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkQueryServicesAdditional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulkQueryServicesAdditional_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "starter_core",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BulkQueryServicesAdditional_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "starter_core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PlanId",
                schema: "starter_core",
                table: "Companies",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_DocumentTypeId",
                schema: "starter_core",
                table: "Lists",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_ListTypeId",
                schema: "starter_core",
                table: "Lists",
                column: "ListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_UserId",
                schema: "starter_core",
                table: "Lists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListTypes_ListGroupId",
                schema: "starter_core",
                table: "ListTypes",
                column: "ListGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ListTypes_PeriodicityId",
                schema: "starter_core",
                table: "ListTypes",
                column: "PeriodicityId");

            migrationBuilder.CreateIndex(
                name: "IX_NewCategories_UserId",
                schema: "starter_core",
                table: "NewCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_NewCategoryId",
                schema: "starter_core",
                table: "News",
                column: "NewCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_News_NewStatusId",
                schema: "starter_core",
                table: "News",
                column: "NewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_News_NewTypeId",
                schema: "starter_core",
                table: "News",
                column: "NewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                schema: "starter_core",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_CompanyId",
                schema: "starter_core",
                table: "NotificationSettings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_UserId",
                schema: "starter_core",
                table: "NotificationSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnLists_CompanyId",
                schema: "starter_core",
                table: "OwnLists",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnLists_OwnListTypeId",
                schema: "starter_core",
                table: "OwnLists",
                column: "OwnListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnLists_UserId",
                schema: "starter_core",
                table: "OwnLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnListTypes_CompanyId",
                schema: "starter_core",
                table: "OwnListTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnListTypes_UserId",
                schema: "starter_core",
                table: "OwnListTypes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_UserId",
                schema: "starter_core",
                table: "Plans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_CompanyId",
                schema: "starter_core",
                table: "Queries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_UserId",
                schema: "starter_core",
                table: "Queries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryDetails_QueryId",
                schema: "starter_core",
                table: "QueryDetails",
                column: "QueryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenus_MenuId",
                schema: "starter_core",
                table: "SubMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "starter_core",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_UserId",
                schema: "starter_core",
                table: "UserPhotos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "starter_core",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                schema: "starter_core",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Plans_PlanId",
                schema: "starter_core",
                table: "Companies",
                column: "PlanId",
                principalSchema: "starter_core",
                principalTable: "Plans",
                principalColumn: "Id");
            migrationBuilder.Sql(SeedData.Initial("starter_core"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Plans_PlanId",
                schema: "starter_core",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "Lists",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "News",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "NotificationSettings",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "OwnLists",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "QueryDetails",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "SubMenus",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "UserPhotos",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "DocumentTypes",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "ListTypes",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "NewCategories",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "NewStatuses",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "NewTypes",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "OwnListTypes",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Queries",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "ListGroups",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Periodicities",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Plans",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "starter_core");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "starter_core");
        }
    }
}
