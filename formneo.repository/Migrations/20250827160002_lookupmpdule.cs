using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class lookupmpdule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LookupCategories_MainClientId_Key",
                table: "LookupCategories");

            migrationBuilder.DropColumn(
                name: "Module",
                table: "LookupCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "ModuleId",
                table: "LookupCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LookupModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_LookupModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookupModules_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupModules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LookupModules_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 15, 59, 59, 120, DateTimeKind.Utc).AddTicks(7677));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 15, 59, 59, 120, DateTimeKind.Utc).AddTicks(5160));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 15, 59, 59, 120, DateTimeKind.Utc).AddTicks(7897));

            migrationBuilder.CreateIndex(
                name: "IX_LookupCategories_MainClientId_ModuleId_Key",
                table: "LookupCategories",
                columns: new[] { "MainClientId", "ModuleId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupCategories_ModuleId",
                table: "LookupCategories",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupModules_CompanyId",
                table: "LookupModules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupModules_MainClientId_Key",
                table: "LookupModules",
                columns: new[] { "MainClientId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupModules_PlantId",
                table: "LookupModules",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookupCategories_LookupModules_ModuleId",
                table: "LookupCategories",
                column: "ModuleId",
                principalTable: "LookupModules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupCategories_LookupModules_ModuleId",
                table: "LookupCategories");

            migrationBuilder.DropTable(
                name: "LookupModules");

            migrationBuilder.DropIndex(
                name: "IX_LookupCategories_MainClientId_ModuleId_Key",
                table: "LookupCategories");

            migrationBuilder.DropIndex(
                name: "IX_LookupCategories_ModuleId",
                table: "LookupCategories");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "LookupCategories");

            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "LookupCategories",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 15, 11, 53, 611, DateTimeKind.Utc).AddTicks(670));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 15, 11, 53, 610, DateTimeKind.Utc).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 27, 15, 11, 53, 611, DateTimeKind.Utc).AddTicks(880));

            migrationBuilder.CreateIndex(
                name: "IX_LookupCategories_MainClientId_Key",
                table: "LookupCategories",
                columns: new[] { "MainClientId", "Key" },
                unique: true);
        }
    }
}
