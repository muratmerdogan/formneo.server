using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class lookup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyType",
                table: "Customers",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionCode",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultNotificationEmail",
                table: "Customers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReferenceCustomer",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LegalName",
                table: "Customers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinUrl",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtsInstitutionNumber",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LookupCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsTenantScoped = table.Column<bool>(type: "boolean", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_LookupCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookupCategories_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupCategories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupCategories_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LookupItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NameLocalizedJson = table.Column<string>(type: "text", nullable: true),
                    OrderNo = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ExternalKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
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
                    table.PrimaryKey("PK_LookupItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookupItems_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupItems_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupItems_LookupCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "LookupCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupItems_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 30, 38, 868, DateTimeKind.Utc).AddTicks(6883));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 30, 38, 868, DateTimeKind.Utc).AddTicks(4782));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 15, 30, 38, 868, DateTimeKind.Utc).AddTicks(7075));

            migrationBuilder.CreateIndex(
                name: "IX_LookupCategories_CompanyId",
                table: "LookupCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupCategories_MainClientId_Key",
                table: "LookupCategories",
                columns: new[] { "MainClientId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupCategories_PlantId",
                table: "LookupCategories",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItems_CategoryId",
                table: "LookupItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItems_CompanyId",
                table: "LookupItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItems_MainClientId_CategoryId_Code",
                table: "LookupItems",
                columns: new[] { "MainClientId", "CategoryId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupItems_PlantId",
                table: "LookupItems",
                column: "PlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LookupItems");

            migrationBuilder.DropTable(
                name: "LookupCategories");

            migrationBuilder.DropColumn(
                name: "CompanyType",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ConnectionCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DefaultNotificationEmail",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsReferenceCustomer",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LegalName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LinkedinUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UtsInstitutionNumber",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 10, 50, 12, 888, DateTimeKind.Utc).AddTicks(3222));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 10, 50, 12, 887, DateTimeKind.Utc).AddTicks(8259));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 26, 10, 50, 12, 888, DateTimeKind.Utc).AddTicks(3735));
        }
    }
}
