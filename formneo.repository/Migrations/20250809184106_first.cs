using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshToken", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApproveItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApproveUser = table.Column<string>(type: "text", nullable: true),
                    ApproveUserNameSurname = table.Column<string>(type: "text", nullable: true),
                    ApprovedUser_Runtime = table.Column<string>(type: "text", nullable: true),
                    ApprovedUser_RuntimeNameSurname = table.Column<string>(type: "text", nullable: true),
                    ApprovedUser_RuntimeNote = table.Column<string>(type: "text", nullable: true),
                    ApprovedUser_RuntimeNumberManDay = table.Column<string>(type: "text", nullable: true),
                    WorkFlowDescription = table.Column<string>(type: "text", nullable: false),
                    ApproverStatus = table.Column<int>(type: "integer", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRolesMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MenuId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    CanView = table.Column<bool>(type: "boolean", nullable: false),
                    CanAdd = table.Column<bool>(type: "boolean", nullable: false),
                    CanEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CanDelete = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRolesMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRolesMenu_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    isSystemAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    canSsoLogin = table.Column<bool>(type: "boolean", nullable: false),
                    isBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    isTestData = table.Column<bool>(type: "boolean", nullable: true),
                    vacationMode = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastLoginIp = table.Column<string>(type: "text", nullable: true),
                    manager1 = table.Column<string>(type: "text", nullable: true),
                    manager2 = table.Column<string>(type: "text", nullable: true),
                    profileInfo = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    FacebookUrl = table.Column<string>(type: "text", nullable: true),
                    InstagramUrl = table.Column<string>(type: "text", nullable: true),
                    TwitterUrl = table.Column<string>(type: "text", nullable: true),
                    LinkedinUrl = table.Column<string>(type: "text", nullable: true),
                    photo = table.Column<string>(type: "text", nullable: true),
                    Department = table.Column<string>(type: "text", nullable: true),
                    SAPDepartmentText = table.Column<string>(type: "text", nullable: true),
                    SAPPositionText = table.Column<string>(type: "text", nullable: true),
                    TicketDepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    HasTicketPermission = table.Column<bool>(type: "boolean", nullable: false),
                    HasDepartmentPermission = table.Column<bool>(type: "boolean", nullable: false),
                    HasOtherCompanyPermission = table.Column<bool>(type: "boolean", nullable: false),
                    HasOtherDeptCalendarPerm = table.Column<bool>(type: "boolean", nullable: false),
                    ResetPasswordCode = table.Column<string>(type: "text", nullable: true),
                    ResetCodeExpiry = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserLevel = table.Column<int>(type: "integer", nullable: false),
                    mainManagerUserAppId = table.Column<Guid>(type: "uuid", nullable: true),
                    canEditTicket = table.Column<bool>(type: "boolean", nullable: false),
                    DontApplyDefaultFilters = table.Column<bool>(type: "boolean", nullable: false),
                    PCname = table.Column<string>(type: "text", nullable: true),
                    DepartmentsId = table.Column<Guid>(type: "uuid", nullable: true),
                    FormAuthId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Plan = table.Column<int>(type: "integer", nullable: false),
                    Timezone = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    OwnerUserId = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    CustomDomain = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DomainVerified = table.Column<bool>(type: "boolean", nullable: false),
                    FeatureFlags = table.Column<string>(type: "text", nullable: false),
                    Quotas = table.Column<string>(type: "text", nullable: false),
                    BillingCustomerId = table.Column<string>(type: "text", nullable: true),
                    BillingEmail = table.Column<string>(type: "text", nullable: true),
                    SsoType = table.Column<int>(type: "integer", nullable: true),
                    SsoMetadataUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plant_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetAdminUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsDoProxy = table.Column<bool>(type: "boolean", nullable: false),
                    ProxyUser = table.Column<string>(type: "text", nullable: false),
                    AdminLevel = table.Column<int>(type: "integer", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetAdminUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetAdminUser_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetAdminUser_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetAdminUser_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetPeriod",
                columns: table => new
                {
                    PeriodCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPeriod", x => x.PeriodCode);
                    table.ForeignKey(
                        name: "FK_BudgetPeriod_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetPeriod_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetPeriod_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    DepartmentText = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Kanban",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    RankId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AssigneeId = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kanban_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kanban_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kanban_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kanban_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MenuCode = table.Column<string>(type: "text", nullable: false),
                    ParentMenuId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Route = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Href = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Icon = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ShowMenu = table.Column<bool>(type: "boolean", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Menus_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "Menus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Menus_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PCTrack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PCname = table.Column<string>(type: "text", nullable: true),
                    ProcessTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProcessType = table.Column<int>(type: "integer", nullable: true),
                    LoginType = table.Column<int>(type: "integer", nullable: true),
                    LoginProcessName = table.Column<string>(type: "text", nullable: true),
                    LoginId = table.Column<string>(type: "text", nullable: true),
                    SubjectLoginId = table.Column<string>(type: "text", nullable: true),
                    XmlData = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCTrack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PCTrack_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PCTrack_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PCTrack_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCategories_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectCategories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectCategories_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProjectGain = table.Column<string>(type: "text", nullable: false),
                    ProjectLearn = table.Column<string>(type: "text", nullable: false),
                    ProjectTags = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketRuleEngine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RuleName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    RuleJson = table.Column<string>(type: "text", nullable: false),
                    AssignedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedDepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    createEnvironment = table.Column<int>(type: "integer", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRuleEngine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRuleEngine_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketRuleEngine_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketRuleEngine_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowDefination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowName = table.Column<string>(type: "text", nullable: true),
                    Defination = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Revision = table.Column<int>(type: "integer", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowDefination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowDefination_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowDefination_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkFlowDefination_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetPeriodUser",
                columns: table => new
                {
                    BudgetPeriodCode = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    requestType = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission = table.Column<int>(type: "integer", nullable: false),
                    processType = table.Column<int>(type: "integer", nullable: false),
                    nameSurname = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPeriodUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetPeriodUser_BudgetPeriod_BudgetPeriodCode",
                        column: x => x.BudgetPeriodCode,
                        principalTable: "BudgetPeriod",
                        principalColumn: "PeriodCode");
                    table.ForeignKey(
                        name: "FK_BudgetPeriodUser_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetPeriodUser_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetPeriodUser_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersId = table.Column<string>(type: "text", nullable: false),
                    CitizenshipNumber = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DepartmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManagerPersId = table.Column<string>(type: "text", nullable: true),
                    SecondManagerPersId = table.Column<string>(type: "text", nullable: true),
                    Photo = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OfficialBirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Workstartdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    WorkendDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactPerson = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "text", nullable: true),
                    BankName = table.Column<string>(type: "text", nullable: true),
                    BankIBAN = table.Column<string>(type: "text", nullable: true),
                    BloodGroup = table.Column<string>(type: "text", nullable: true),
                    RelatedPerson = table.Column<string>(type: "text", nullable: true),
                    illness = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    DeptCode = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormName = table.Column<string>(type: "text", nullable: false),
                    FormDescription = table.Column<string>(type: "text", nullable: false),
                    Revision = table.Column<int>(type: "integer", nullable: false),
                    FormDesign = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<int>(type: "integer", nullable: false),
                    JavaScriptCode = table.Column<string>(type: "text", nullable: false),
                    FormType = table.Column<int>(type: "integer", nullable: false),
                    FormCategory = table.Column<int>(type: "integer", nullable: false),
                    FormPriority = table.Column<int>(type: "integer", nullable: false),
                    WorkFlowDefinationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentFormId = table.Column<Guid>(type: "uuid", nullable: true),
                    CanEdit = table.Column<bool>(type: "boolean", nullable: false),
                    ShowInMenu = table.Column<bool>(type: "boolean", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Form_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Form_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Form_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Form_WorkFlowDefination_WorkFlowDefinationId",
                        column: x => x.WorkFlowDefinationId,
                        principalTable: "WorkFlowDefination",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormRuleEngine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkFlowDefinationId = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rulejson = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormRuleEngine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormRuleEngine_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormRuleEngine_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormRuleEngine_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormRuleEngine_WorkFlowDefination_WorkFlowDefinationId",
                        column: x => x.WorkFlowDefinationId,
                        principalTable: "WorkFlowDefination",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowHead",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowName = table.Column<string>(type: "text", nullable: true),
                    CurrentNodeId = table.Column<string>(type: "text", nullable: true),
                    CurrentNodeName = table.Column<string>(type: "text", nullable: true),
                    workFlowStatus = table.Column<int>(type: "integer", nullable: true),
                    WorkFlowInfo = table.Column<string>(type: "text", nullable: true),
                    CreateUser = table.Column<string>(type: "text", nullable: false),
                    WorkFlowDefinationId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkFlowDefinationJson = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowHead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowHead_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowHead_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowHead_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowHead_WorkFlowDefination_WorkFlowDefinationId",
                        column: x => x.WorkFlowDefinationId,
                        principalTable: "WorkFlowDefination",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpSalary",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric(18,4)", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpSalary", x => new { x.EmployeeId, x.StartDate, x.EndDate });
                    table.ForeignKey(
                        name: "FK_EmpSalary_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpSalary_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpSalary_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpSalary_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormAssign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAppId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FormRunTimeId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormAssign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormAssign_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormAssign_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormAssign_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormAssign_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormAssign_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormAuth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserIdsSerialized = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormAuth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormAuth_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormAuth_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormAuth_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormAuth_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormRuntime",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValuesJson = table.Column<string>(type: "text", nullable: false),
                    ValuesJsonData = table.Column<string>(type: "text", nullable: false),
                    İsActive = table.Column<bool>(type: "boolean", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormRuntime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormRuntime_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormRuntime_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormRuntime_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormRuntime_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetJobCodeRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobCode = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Name_Ru_RU = table.Column<string>(type: "text", nullable: false),
                    Name_En_Debug = table.Column<string>(type: "text", nullable: false),
                    Name_Tr_TR = table.Column<string>(type: "text", nullable: false),
                    Name_En_US = table.Column<string>(type: "text", nullable: false),
                    Description_En_Debug = table.Column<string>(type: "text", nullable: false),
                    Description_En_US = table.Column<string>(type: "text", nullable: false),
                    Description_Ru_RU = table.Column<string>(type: "text", nullable: false),
                    Description_Tr_TR = table.Column<string>(type: "text", nullable: false),
                    RequestReason = table.Column<string>(type: "text", nullable: false),
                    IsSend = table.Column<bool>(type: "boolean", nullable: false),
                    WorkflowHeadId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsFullTime = table.Column<bool>(type: "boolean", nullable: false),
                    RegularTemporary = table.Column<string>(type: "text", nullable: false),
                    DefaultEmployeeClass = table.Column<string>(type: "text", nullable: false),
                    IsFulltimeEmployee = table.Column<bool>(type: "boolean", nullable: false),
                    Grade = table.Column<string>(type: "text", nullable: false),
                    JobFunction = table.Column<string>(type: "text", nullable: false),
                    PositionLevel = table.Column<string>(type: "text", nullable: false),
                    Cust_Joblevelgroup = table.Column<string>(type: "text", nullable: false),
                    Cust_Metin = table.Column<string>(type: "text", nullable: false),
                    Cust_Jobcode = table.Column<string>(type: "text", nullable: false),
                    Cust_AdinesStatus = table.Column<string>(type: "text", nullable: false),
                    Cust_EmploymentType = table.Column<string>(type: "text", nullable: false),
                    Cust_GorevBirimTipi = table.Column<string>(type: "text", nullable: false),
                    Cust_IsManager = table.Column<bool>(type: "boolean", nullable: false),
                    Cust_Bolum = table.Column<string>(type: "text", nullable: false),
                    Cust_Ronesanskademe = table.Column<string>(type: "text", nullable: false),
                    Cust_Haykademe = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetJobCodeRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetJobCodeRequest_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetJobCodeRequest_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetJobCodeRequest_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetJobCodeRequest_WorkflowHead_WorkflowHeadId",
                        column: x => x.WorkflowHeadId,
                        principalTable: "WorkflowHead",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetNormCodeRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    effectiveStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cust_IseBaslamaTarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cust_PlanlananIseGiris = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cust_plannedEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cust_actualhiredate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    effectiveStatus = table.Column<string>(type: "text", nullable: true),
                    vacant = table.Column<bool>(type: "boolean", nullable: true),
                    changeReason = table.Column<string>(type: "text", nullable: true),
                    cust_GeoZone = table.Column<string>(type: "text", nullable: true),
                    cust_company = table.Column<string>(type: "text", nullable: true),
                    externalName_tr_TR = table.Column<string>(type: "text", nullable: true),
                    externalName_defaultValue = table.Column<string>(type: "text", nullable: true),
                    externalName_en_US = table.Column<string>(type: "text", nullable: true),
                    externalName_en_DEBUG = table.Column<string>(type: "text", nullable: true),
                    externalName_ru_RU = table.Column<string>(type: "text", nullable: true),
                    multipleIncumbentsAllowed = table.Column<bool>(type: "boolean", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    targetFTE = table.Column<string>(type: "text", nullable: true),
                    standardHours = table.Column<string>(type: "text", nullable: true),
                    jobCode = table.Column<string>(type: "text", nullable: true),
                    cust_jobfunction = table.Column<string>(type: "text", nullable: true),
                    cust_ronesansjoblevel = table.Column<string>(type: "text", nullable: true),
                    cust_ronesansKademe = table.Column<string>(type: "text", nullable: true),
                    payGrade = table.Column<string>(type: "text", nullable: true),
                    jobTitle = table.Column<string>(type: "text", nullable: true),
                    employeeClass = table.Column<string>(type: "text", nullable: true),
                    cust_empSubGroup = table.Column<string>(type: "text", nullable: true),
                    cust_EmpGroup = table.Column<string>(type: "text", nullable: true),
                    cust_companyGroup = table.Column<string>(type: "text", nullable: true),
                    cust_customlegalEntity = table.Column<string>(type: "text", nullable: true),
                    businessUnit = table.Column<string>(type: "text", nullable: true),
                    division = table.Column<string>(type: "text", nullable: true),
                    cust_sub_division = table.Column<string>(type: "text", nullable: true),
                    department = table.Column<string>(type: "text", nullable: true),
                    cust_parentDepartment2 = table.Column<string>(type: "text", nullable: true),
                    cust_parentDepartment = table.Column<string>(type: "text", nullable: true),
                    costCenter = table.Column<string>(type: "text", nullable: true),
                    cust_locationGroup = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    cust_calismaYeriTuru = table.Column<string>(type: "text", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    cust_payGroup = table.Column<string>(type: "text", nullable: true),
                    cust_isAlani = table.Column<string>(type: "text", nullable: true),
                    cust_phisicalLocation = table.Column<string>(type: "text", nullable: true),
                    cust_ticket = table.Column<string>(type: "text", nullable: true),
                    cust_HayKademe = table.Column<string>(type: "text", nullable: true),
                    cust_ChiefPosition = table.Column<bool>(type: "boolean", nullable: true),
                    parentPosition = table.Column<string>(type: "text", nullable: true),
                    WorkflowHeadId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsSend = table.Column<bool>(type: "boolean", nullable: false),
                    ProcessType = table.Column<int>(type: "integer", nullable: true),
                    IsInternalSource = table.Column<bool>(type: "boolean", nullable: true),
                    InternalEmploymentType = table.Column<int>(type: "integer", nullable: true),
                    relationManager = table.Column<string>(type: "text", nullable: true),
                    relationEmployess = table.Column<string>(type: "text", nullable: true),
                    Hardware = table.Column<string>(type: "text", nullable: true),
                    Licence = table.Column<string>(type: "text", nullable: true),
                    internalSourceEmp = table.Column<string>(type: "text", nullable: true),
                    jobCodeDescription = table.Column<string>(type: "text", nullable: true),
                    promotionPeriod = table.Column<string>(type: "text", nullable: true),
                    promotionPeriodTxt = table.Column<string>(type: "text", nullable: true),
                    propotionReasonTxt = table.Column<string>(type: "text", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    isTransferred = table.Column<bool>(type: "boolean", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetNormCodeRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetNormCodeRequest_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetNormCodeRequest_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetNormCodeRequest_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetNormCodeRequest_WorkflowHead_WorkflowHeadId",
                        column: x => x.WorkflowHeadId,
                        principalTable: "WorkflowHead",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetPromotionRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpCode = table.Column<string>(type: "text", nullable: false),
                    PositionCode = table.Column<string>(type: "text", nullable: false),
                    PromotionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WorkflowHeadId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsSend = table.Column<bool>(type: "boolean", nullable: false),
                    ManagerUser = table.Column<string>(type: "text", nullable: false),
                    TeamUsers = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPromotionRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetPromotionRequest_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetPromotionRequest_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetPromotionRequest_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetPromotionRequest_WorkflowHead_WorkflowHeadId",
                        column: x => x.WorkflowHeadId,
                        principalTable: "WorkflowHead",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowHeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<string>(type: "text", nullable: false),
                    NodeName = table.Column<string>(type: "text", nullable: false),
                    NodeType = table.Column<string>(type: "text", nullable: false),
                    NodeDescription = table.Column<string>(type: "text", nullable: false),
                    workFlowNodeStatus = table.Column<int>(type: "integer", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowItem_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowItem_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowItem_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowItem_WorkflowHead_WorkflowHeadId",
                        column: x => x.WorkflowHeadId,
                        principalTable: "WorkflowHead",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketDepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetTag = table.Column<string>(type: "text", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CPU = table.Column<string>(type: "text", nullable: true),
                    RAM = table.Column<int>(type: "integer", nullable: true),
                    DiskType = table.Column<int>(type: "integer", nullable: true),
                    DiskSize = table.Column<int>(type: "integer", nullable: true),
                    GPU = table.Column<string>(type: "text", nullable: true),
                    MACAddress = table.Column<string>(type: "text", nullable: true),
                    StaticIPAddress = table.Column<string>(type: "text", nullable: true),
                    OperatingSystem = table.Column<string>(type: "text", nullable: true),
                    OS_LicenseStatus = table.Column<int>(type: "integer", nullable: true),
                    OfficeLicense = table.Column<string>(type: "text", nullable: true),
                    UserAppId = table.Column<string>(type: "text", nullable: true),
                    TicketDepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    OfficeLocation = table.Column<int>(type: "integer", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InvoiceOrVendor = table.Column<string>(type: "text", nullable: true),
                    WarrantyEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AssetNumber = table.Column<string>(type: "text", nullable: true),
                    LastMaintenanceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    QRorBarcode = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CustomerRefId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Positions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Positions_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    taskId = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    Progress = table.Column<int>(type: "integer", nullable: true),
                    Predecessor = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<string>(type: "text", nullable: true),
                    Milestone = table.Column<bool>(type: "boolean", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    IsManual = table.Column<bool>(type: "boolean", nullable: true),
                    UserIds = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketApprove",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAppId = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketApprove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketApprove_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketApprove_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketApprove_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketApprove_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketAssigne",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAppId = table.Column<string>(type: "text", nullable: true),
                    TicketTeamID = table.Column<Guid>(type: "uuid", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAssigne", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketAssigne_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketAssigne_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketAssigne_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketAssigne_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketId = table.Column<Guid>(type: "uuid", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketComment_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketComment_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketComment_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Base64 = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketFile_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketFile_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketFile_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketFile_TicketComment_TicketCommentId",
                        column: x => x.TicketCommentId,
                        principalTable: "TicketComment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeparmentCode = table.Column<string>(type: "text", nullable: false),
                    DepartmentText = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ManagerId = table.Column<string>(type: "text", nullable: true),
                    WorkCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentDepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsVisibleInList = table.Column<bool>(type: "boolean", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketDepartment_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketDepartment_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketDepartment_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketDepartment_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketDepartment_TicketDepartment_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalTable: "TicketDepartment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAppId = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketNotifications_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketNotifications_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketNotifications_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketNotifications_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SubProjectName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Risks = table.Column<string>(type: "text", nullable: true),
                    ReportsUrl = table.Column<string>(type: "text", nullable: true),
                    WorkCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagerId = table.Column<string>(type: "text", nullable: true),
                    UserIds = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    ProjectCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketProjects_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketProjects_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketProjects_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketProjects_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketProjects_ProjectCategories_ProjectCategoryId",
                        column: x => x.ProjectCategoryId,
                        principalTable: "ProjectCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketCode = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WorkCompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketDepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkCompanySystemInfoId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserAppId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    TicketSubject = table.Column<int>(type: "integer", nullable: false),
                    TicketSLA = table.Column<int>(type: "integer", nullable: false),
                    ActualStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApproveStatus = table.Column<int>(type: "integer", nullable: false),
                    TicketAssigneId = table.Column<Guid>(type: "uuid", nullable: true),
                    TicketApproveId = table.Column<Guid>(type: "uuid", nullable: true),
                    isTeam = table.Column<bool>(type: "boolean", nullable: false),
                    isApprove = table.Column<bool>(type: "boolean", nullable: false),
                    WorkflowHeadId = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomerRefId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsFromEmail = table.Column<bool>(type: "boolean", nullable: false),
                    MailConversationId = table.Column<string>(type: "text", nullable: true),
                    AddedMailAddresses = table.Column<string>(type: "text", nullable: true),
                    IsFilePath = table.Column<bool>(type: "boolean", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    EstimatedDeadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TicketProjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketDepartment_TicketDepartmentId",
                        column: x => x.TicketDepartmentId,
                        principalTable: "TicketDepartment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketProjects_TicketProjectId",
                        column: x => x.TicketProjectId,
                        principalTable: "TicketProjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_WorkflowHead_WorkflowHeadId",
                        column: x => x.WorkflowHeadId,
                        principalTable: "WorkflowHead",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketTeam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagerId = table.Column<string>(type: "text", nullable: false),
                    WorkCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTeam_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeam_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeam_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeam_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeam_TicketDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "TicketDepartment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketTeamUserApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAppId = table.Column<string>(type: "text", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTeamUserApp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTeamUserApp_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeamUserApp_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeamUserApp_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeamUserApp_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTeamUserApp_TicketTeam_TicketTeamId",
                        column: x => x.TicketTeamId,
                        principalTable: "TicketTeam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCalendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CustomerRefId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserAppId = table.Column<string>(type: "text", nullable: false),
                    Percentage = table.Column<string>(type: "text", nullable: true),
                    WorkLocation = table.Column<int>(type: "integer", nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCalendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCalendar_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCalendar_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCalendar_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCalendar_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ApproveWorkDesign = table.Column<int>(type: "integer", nullable: false),
                    UserAppId = table.Column<string>(type: "text", nullable: true),
                    WorkFlowDefinationId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    WorkCompanyTicketMatrisId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCompany_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompany_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompany_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompany_WorkFlowDefination_WorkFlowDefinationId",
                        column: x => x.WorkFlowDefinationId,
                        principalTable: "WorkFlowDefination",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkCompanySystemInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WorkCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCompanySystemInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCompanySystemInfo_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompanySystemInfo_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompanySystemInfo_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompanySystemInfo_WorkCompany_WorkCompanyId",
                        column: x => x.WorkCompanyId,
                        principalTable: "WorkCompany",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkCompanyTicketMatris",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FromCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    ToCompaniesIdsSerialized = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCompanyTicketMatris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCompanyTicketMatris_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompanyTicketMatris_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompanyTicketMatris_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkCompanyTicketMatris_WorkCompany_FromCompanyId",
                        column: x => x.FromCompanyId,
                        principalTable: "WorkCompany",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "BillingCustomerId", "BillingEmail", "CreatedDate", "CustomDomain", "DomainVerified", "Email", "FeatureFlags", "IsActive", "LogoUrl", "Name", "OwnerUserId", "PhoneNumber", "Plan", "Quotas", "Slug", "SsoMetadataUrl", "SsoType", "Status", "Timezone", "UpdatedDate" },
                values: new object[] { new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"), null, null, new DateTime(2025, 8, 9, 18, 41, 5, 758, DateTimeKind.Utc).AddTicks(3956), null, false, "info@vesacons.com", "{}", true, null, "RonesansHolding", null, "5069112452", 0, "{}", null, null, null, 0, "Europe/Istanbul", null });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "ClientId", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"), new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"), new DateTime(2025, 8, 9, 18, 41, 5, 758, DateTimeKind.Utc).AddTicks(1428), "RonesansHolding", null });

            migrationBuilder.InsertData(
                table: "Plant",
                columns: new[] { "Id", "CompanyId", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"), new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"), new DateTime(2025, 8, 9, 18, 41, 5, 758, DateTimeKind.Utc).AddTicks(4242), "RonesansHoldingTurkey", null });

            migrationBuilder.CreateIndex(
                name: "IX_ApproveItems_CompanyId",
                table: "ApproveItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveItems_MainClientId",
                table: "ApproveItems",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveItems_PlantId",
                table: "ApproveItems",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveItems_WorkflowItemId",
                table: "ApproveItems",
                column: "WorkflowItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesMenu_CompanyId",
                table: "AspNetRolesMenu",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesMenu_MainClientId",
                table: "AspNetRolesMenu",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesMenu_MenuId_RoleId",
                table: "AspNetRolesMenu",
                columns: new[] { "MenuId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesMenu_PlantId",
                table: "AspNetRolesMenu",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesMenu_RoleId",
                table: "AspNetRolesMenu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentsId",
                table: "AspNetUsers",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FormAuthId",
                table: "AspNetUsers",
                column: "FormAuthId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TicketDepartmentId",
                table: "AspNetUsers",
                column: "TicketDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkCompanyId",
                table: "AspNetUsers",
                column: "WorkCompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAdminUser_CompanyId",
                table: "BudgetAdminUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAdminUser_MainClientId",
                table: "BudgetAdminUser",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAdminUser_PlantId",
                table: "BudgetAdminUser",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJobCodeRequest_CompanyId",
                table: "BudgetJobCodeRequest",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJobCodeRequest_MainClientId",
                table: "BudgetJobCodeRequest",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJobCodeRequest_PlantId",
                table: "BudgetJobCodeRequest",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJobCodeRequest_WorkflowHeadId",
                table: "BudgetJobCodeRequest",
                column: "WorkflowHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetNormCodeRequest_CompanyId",
                table: "BudgetNormCodeRequest",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetNormCodeRequest_MainClientId",
                table: "BudgetNormCodeRequest",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetNormCodeRequest_PlantId",
                table: "BudgetNormCodeRequest",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetNormCodeRequest_WorkflowHeadId",
                table: "BudgetNormCodeRequest",
                column: "WorkflowHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriod_CompanyId",
                table: "BudgetPeriod",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriod_MainClientId",
                table: "BudgetPeriod",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriod_PlantId",
                table: "BudgetPeriod",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_BudgetPeriodCode_UserName_requestType",
                table: "BudgetPeriodUser",
                columns: new[] { "BudgetPeriodCode", "UserName", "requestType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_CompanyId",
                table: "BudgetPeriodUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_MainClientId",
                table: "BudgetPeriodUser",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_PlantId",
                table: "BudgetPeriodUser",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionRequest_CompanyId",
                table: "BudgetPromotionRequest",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionRequest_MainClientId",
                table: "BudgetPromotionRequest",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionRequest_PlantId",
                table: "BudgetPromotionRequest",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionRequest_WorkflowHeadId",
                table: "BudgetPromotionRequest",
                column: "WorkflowHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_OwnerUserId",
                table: "Clients",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ClientId",
                table: "Companies",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_MainClientId",
                table: "Departments",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_PlantId",
                table: "Departments",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_CompanyId",
                table: "DepartmentUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_MainClientId",
                table: "DepartmentUsers",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_PlantId",
                table: "DepartmentUsers",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_TicketDepartmentId",
                table: "DepartmentUsers",
                column: "TicketDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_UserId",
                table: "DepartmentUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompanyId",
                table: "Employee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentsId",
                table: "Employee",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MainClientId",
                table: "Employee",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PlantId",
                table: "Employee",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpSalary_CompanyId",
                table: "EmpSalary",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpSalary_MainClientId",
                table: "EmpSalary",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpSalary_PlantId",
                table: "EmpSalary",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_CompanyId",
                table: "Form",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_MainClientId",
                table: "Form",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_PlantId",
                table: "Form",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_WorkFlowDefinationId",
                table: "Form",
                column: "WorkFlowDefinationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormAssign_CompanyId",
                table: "FormAssign",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAssign_FormId",
                table: "FormAssign",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAssign_MainClientId",
                table: "FormAssign",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAssign_PlantId",
                table: "FormAssign",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAssign_UserAppId",
                table: "FormAssign",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAuth_CompanyId",
                table: "FormAuth",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAuth_FormId",
                table: "FormAuth",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAuth_MainClientId",
                table: "FormAuth",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAuth_PlantId",
                table: "FormAuth",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuleEngine_CompanyId",
                table: "FormRuleEngine",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuleEngine_MainClientId",
                table: "FormRuleEngine",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuleEngine_PlantId",
                table: "FormRuleEngine",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuleEngine_WorkFlowDefinationId",
                table: "FormRuleEngine",
                column: "WorkFlowDefinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuntime_CompanyId",
                table: "FormRuntime",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuntime_FormId",
                table: "FormRuntime",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuntime_MainClientId",
                table: "FormRuntime",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuntime_PlantId",
                table: "FormRuntime",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CompanyId",
                table: "Inventory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MainClientId",
                table: "Inventory",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PlantId",
                table: "Inventory",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_TicketDepartmentId",
                table: "Inventory",
                column: "TicketDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_UserAppId",
                table: "Inventory",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanban_AssigneeId",
                table: "Kanban",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanban_CompanyId",
                table: "Kanban",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanban_MainClientId",
                table: "Kanban",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanban_PlantId",
                table: "Kanban",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_CompanyId",
                table: "Menus",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MainClientId",
                table: "Menus",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentMenuId",
                table: "Menus",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_PlantId",
                table: "Menus",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PCTrack_CompanyId",
                table: "PCTrack",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PCTrack_MainClientId",
                table: "PCTrack",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PCTrack_PlantId",
                table: "PCTrack",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_CompanyId",
                table: "Plant",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CompanyId",
                table: "Positions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CustomerRefId",
                table: "Positions",
                column: "CustomerRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_MainClientId",
                table: "Positions",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PlantId",
                table: "Positions",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategories_CompanyId",
                table: "ProjectCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategories_MainClientId",
                table: "ProjectCategories",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategories_PlantId",
                table: "ProjectCategories",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_MainClientId",
                table: "Projects",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PlantId",
                table: "Projects",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_CompanyId",
                table: "ProjectTasks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_MainClientId",
                table: "ProjectTasks",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_PlantId",
                table: "ProjectTasks",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketApprove_CompanyId",
                table: "TicketApprove",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketApprove_MainClientId",
                table: "TicketApprove",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketApprove_PlantId",
                table: "TicketApprove",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketApprove_TicketsId",
                table: "TicketApprove",
                column: "TicketsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketApprove_UserAppId",
                table: "TicketApprove",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_CompanyId",
                table: "TicketAssigne",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_MainClientId",
                table: "TicketAssigne",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_PlantId",
                table: "TicketAssigne",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_TicketsId",
                table: "TicketAssigne",
                column: "TicketsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_TicketTeamID",
                table: "TicketAssigne",
                column: "TicketTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_UserAppId",
                table: "TicketAssigne",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_CompanyId",
                table: "TicketComment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_MainClientId",
                table: "TicketComment",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_PlantId",
                table: "TicketComment",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_TicketId",
                table: "TicketComment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_CompanyId",
                table: "TicketDepartment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_MainClientId",
                table: "TicketDepartment",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_ManagerId",
                table: "TicketDepartment",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_ParentDepartmentId",
                table: "TicketDepartment",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_PlantId",
                table: "TicketDepartment",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_WorkCompanyId",
                table: "TicketDepartment",
                column: "WorkCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFile_CompanyId",
                table: "TicketFile",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFile_MainClientId",
                table: "TicketFile",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFile_PlantId",
                table: "TicketFile",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFile_TicketCommentId",
                table: "TicketFile",
                column: "TicketCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotifications_CompanyId",
                table: "TicketNotifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotifications_MainClientId",
                table: "TicketNotifications",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotifications_PlantId",
                table: "TicketNotifications",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotifications_TicketId",
                table: "TicketNotifications",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotifications_UserAppId",
                table: "TicketNotifications",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_CompanyId",
                table: "TicketProjects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_MainClientId",
                table: "TicketProjects",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_ManagerId",
                table: "TicketProjects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_PlantId",
                table: "TicketProjects",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_ProjectCategoryId",
                table: "TicketProjects",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_WorkCompanyId",
                table: "TicketProjects",
                column: "WorkCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRuleEngine_CompanyId",
                table: "TicketRuleEngine",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRuleEngine_MainClientId",
                table: "TicketRuleEngine",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRuleEngine_PlantId",
                table: "TicketRuleEngine",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CompanyId",
                table: "Tickets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerRefId",
                table: "Tickets",
                column: "CustomerRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MainClientId",
                table: "Tickets",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PlantId",
                table: "Tickets",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketDepartmentId",
                table: "Tickets",
                column: "TicketDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketProjectId",
                table: "Tickets",
                column: "TicketProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserAppId",
                table: "Tickets",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_WorkCompanyId",
                table: "Tickets",
                column: "WorkCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_WorkCompanySystemInfoId",
                table: "Tickets",
                column: "WorkCompanySystemInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_WorkflowHeadId",
                table: "Tickets",
                column: "WorkflowHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_CompanyId",
                table: "TicketTeam",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_DepartmentId",
                table: "TicketTeam",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_MainClientId",
                table: "TicketTeam",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_ManagerId",
                table: "TicketTeam",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_PlantId",
                table: "TicketTeam",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_WorkCompanyId",
                table: "TicketTeam",
                column: "WorkCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeamUserApp_CompanyId",
                table: "TicketTeamUserApp",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeamUserApp_MainClientId",
                table: "TicketTeamUserApp",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeamUserApp_PlantId",
                table: "TicketTeamUserApp",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeamUserApp_TicketTeamId_UserAppId",
                table: "TicketTeamUserApp",
                columns: new[] { "TicketTeamId", "UserAppId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeamUserApp_UserAppId",
                table: "TicketTeamUserApp",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendar_CompanyId",
                table: "UserCalendar",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendar_CustomerRefId",
                table: "UserCalendar",
                column: "CustomerRefId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendar_MainClientId",
                table: "UserCalendar",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendar_PlantId",
                table: "UserCalendar",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendar_UserAppId",
                table: "UserCalendar",
                column: "UserAppId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompany_CompanyId",
                table: "WorkCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompany_MainClientId",
                table: "WorkCompany",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompany_PlantId",
                table: "WorkCompany",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompany_WorkCompanyTicketMatrisId",
                table: "WorkCompany",
                column: "WorkCompanyTicketMatrisId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompany_WorkFlowDefinationId",
                table: "WorkCompany",
                column: "WorkFlowDefinationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanySystemInfo_CompanyId",
                table: "WorkCompanySystemInfo",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanySystemInfo_MainClientId",
                table: "WorkCompanySystemInfo",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanySystemInfo_PlantId",
                table: "WorkCompanySystemInfo",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanySystemInfo_WorkCompanyId",
                table: "WorkCompanySystemInfo",
                column: "WorkCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanyTicketMatris_CompanyId",
                table: "WorkCompanyTicketMatris",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanyTicketMatris_FromCompanyId",
                table: "WorkCompanyTicketMatris",
                column: "FromCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanyTicketMatris_MainClientId",
                table: "WorkCompanyTicketMatris",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanyTicketMatris_PlantId",
                table: "WorkCompanyTicketMatris",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDefination_CompanyId",
                table: "WorkFlowDefination",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDefination_MainClientId",
                table: "WorkFlowDefination",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDefination_PlantId",
                table: "WorkFlowDefination",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowHead_CompanyId",
                table: "WorkflowHead",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowHead_MainClientId",
                table: "WorkflowHead",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowHead_PlantId",
                table: "WorkflowHead",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowHead_WorkFlowDefinationId",
                table: "WorkflowHead",
                column: "WorkFlowDefinationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItem_CompanyId",
                table: "WorkflowItem",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItem_MainClientId",
                table: "WorkflowItem",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItem_PlantId",
                table: "WorkflowItem",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItem_WorkflowHeadId",
                table: "WorkflowItem",
                column: "WorkflowHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApproveItems_Clients_MainClientId",
                table: "ApproveItems",
                column: "MainClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApproveItems_Companies_CompanyId",
                table: "ApproveItems",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApproveItems_Plant_PlantId",
                table: "ApproveItems",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApproveItems_WorkflowItem_WorkflowItemId",
                table: "ApproveItems",
                column: "WorkflowItemId",
                principalTable: "WorkflowItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRolesMenu_Clients_MainClientId",
                table: "AspNetRolesMenu",
                column: "MainClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRolesMenu_Companies_CompanyId",
                table: "AspNetRolesMenu",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRolesMenu_Menus_MenuId",
                table: "AspNetRolesMenu",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRolesMenu_Plant_PlantId",
                table: "AspNetRolesMenu",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentsId",
                table: "AspNetUsers",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FormAuth_FormAuthId",
                table: "AspNetUsers",
                column: "FormAuthId",
                principalTable: "FormAuth",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_PositionId",
                table: "AspNetUsers",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TicketDepartment_TicketDepartmentId",
                table: "AspNetUsers",
                column: "TicketDepartmentId",
                principalTable: "TicketDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WorkCompany_WorkCompanyId",
                table: "AspNetUsers",
                column: "WorkCompanyId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUsers_TicketDepartment_TicketDepartmentId",
                table: "DepartmentUsers",
                column: "TicketDepartmentId",
                principalTable: "TicketDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_TicketDepartment_TicketDepartmentId",
                table: "Inventory",
                column: "TicketDepartmentId",
                principalTable: "TicketDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_WorkCompany_CustomerRefId",
                table: "Positions",
                column: "CustomerRefId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_TicketProjects_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId",
                principalTable: "TicketProjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketApprove_Tickets_TicketsId",
                table: "TicketApprove",
                column: "TicketsId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAssigne_TicketTeam_TicketTeamID",
                table: "TicketAssigne",
                column: "TicketTeamID",
                principalTable: "TicketTeam",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAssigne_Tickets_TicketsId",
                table: "TicketAssigne",
                column: "TicketsId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComment_Tickets_TicketId",
                table: "TicketComment",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDepartment_WorkCompany_WorkCompanyId",
                table: "TicketDepartment",
                column: "WorkCompanyId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketNotifications_Tickets_TicketId",
                table: "TicketNotifications",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketProjects_WorkCompany_WorkCompanyId",
                table: "TicketProjects",
                column: "WorkCompanyId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_WorkCompanySystemInfo_WorkCompanySystemInfoId",
                table: "Tickets",
                column: "WorkCompanySystemInfoId",
                principalTable: "WorkCompanySystemInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_WorkCompany_CustomerRefId",
                table: "Tickets",
                column: "CustomerRefId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_WorkCompany_WorkCompanyId",
                table: "Tickets",
                column: "WorkCompanyId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTeam_WorkCompany_WorkCompanyId",
                table: "TicketTeam",
                column: "WorkCompanyId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCalendar_WorkCompany_CustomerRefId",
                table: "UserCalendar",
                column: "CustomerRefId",
                principalTable: "WorkCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCompany_WorkCompanyTicketMatris_WorkCompanyTicketMatris~",
                table: "WorkCompany",
                column: "WorkCompanyTicketMatrisId",
                principalTable: "WorkCompanyTicketMatris",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Clients_ClientId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Clients_MainClientId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Clients_MainClientId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAuth_Clients_MainClientId",
                table: "FormAuth");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Clients_MainClientId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDepartment_Clients_MainClientId",
                table: "TicketDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompany_Clients_MainClientId",
                table: "WorkCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanyTicketMatris_Clients_MainClientId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowDefination_Clients_MainClientId",
                table: "WorkFlowDefination");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Companies_CompanyId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAuth_Companies_CompanyId",
                table: "FormAuth");

            migrationBuilder.DropForeignKey(
                name: "FK_Plant_Companies_CompanyId",
                table: "Plant");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Companies_CompanyId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDepartment_Companies_CompanyId",
                table: "TicketDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompany_Companies_CompanyId",
                table: "WorkCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanyTicketMatris_Companies_CompanyId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowDefination_Companies_CompanyId",
                table: "WorkFlowDefination");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Plant_PlantId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Plant_PlantId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAuth_Plant_PlantId",
                table: "FormAuth");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Plant_PlantId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDepartment_Plant_PlantId",
                table: "TicketDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompany_Plant_PlantId",
                table: "WorkCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanyTicketMatris_Plant_PlantId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowDefination_Plant_PlantId",
                table: "WorkFlowDefination");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDepartment_AspNetUsers_ManagerId",
                table: "TicketDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanyTicketMatris_WorkCompany_FromCompanyId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropTable(
                name: "ApproveItems");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetRolesMenu");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BudgetAdminUser");

            migrationBuilder.DropTable(
                name: "BudgetJobCodeRequest");

            migrationBuilder.DropTable(
                name: "BudgetNormCodeRequest");

            migrationBuilder.DropTable(
                name: "BudgetPeriodUser");

            migrationBuilder.DropTable(
                name: "BudgetPromotionRequest");

            migrationBuilder.DropTable(
                name: "DepartmentUsers");

            migrationBuilder.DropTable(
                name: "EmpSalary");

            migrationBuilder.DropTable(
                name: "FormAssign");

            migrationBuilder.DropTable(
                name: "FormRuleEngine");

            migrationBuilder.DropTable(
                name: "FormRuntime");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Kanban");

            migrationBuilder.DropTable(
                name: "PCTrack");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "TicketApprove");

            migrationBuilder.DropTable(
                name: "TicketAssigne");

            migrationBuilder.DropTable(
                name: "TicketFile");

            migrationBuilder.DropTable(
                name: "TicketNotifications");

            migrationBuilder.DropTable(
                name: "TicketRuleEngine");

            migrationBuilder.DropTable(
                name: "TicketTeamUserApp");

            migrationBuilder.DropTable(
                name: "UserCalendar");

            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.DropTable(
                name: "WorkflowItem");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BudgetPeriod");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "TicketComment");

            migrationBuilder.DropTable(
                name: "TicketTeam");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketProjects");

            migrationBuilder.DropTable(
                name: "WorkCompanySystemInfo");

            migrationBuilder.DropTable(
                name: "WorkflowHead");

            migrationBuilder.DropTable(
                name: "ProjectCategories");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "FormAuth");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "TicketDepartment");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "WorkCompany");

            migrationBuilder.DropTable(
                name: "WorkCompanyTicketMatris");

            migrationBuilder.DropTable(
                name: "WorkFlowDefination");
        }
    }
}
