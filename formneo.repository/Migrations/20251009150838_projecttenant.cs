using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class projecttenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_Projects_ProjectId",
                table: "ProjectTask");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeamMembers_Projects_ProjectId",
                table: "ProjectTeamMembers");

            migrationBuilder.CreateTable(
                name: "TenantProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_TenantProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantProjects_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TenantProjects_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 9, 15, 8, 37, 530, DateTimeKind.Utc).AddTicks(120));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 9, 15, 8, 37, 529, DateTimeKind.Utc).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 9, 15, 8, 37, 530, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.CreateIndex(
                name: "IX_TenantProjects_CustomerId",
                table: "TenantProjects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantProjects_MainClientId",
                table: "TenantProjects",
                column: "MainClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_TenantProjects_ProjectId",
                table: "ProjectTask",
                column: "ProjectId",
                principalTable: "TenantProjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeamMembers_TenantProjects_ProjectId",
                table: "ProjectTeamMembers",
                column: "ProjectId",
                principalTable: "TenantProjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_TenantProjects_ProjectId",
                table: "ProjectTask");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeamMembers_TenantProjects_ProjectId",
                table: "ProjectTeamMembers");

            migrationBuilder.DropTable(
                name: "TenantProjects");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 9, 14, 39, 49, 505, DateTimeKind.Utc).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 9, 14, 39, 49, 505, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 9, 14, 39, 49, 505, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_Projects_ProjectId",
                table: "ProjectTask",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeamMembers_Projects_ProjectId",
                table: "ProjectTeamMembers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
