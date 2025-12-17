using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class forminstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkflowHeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: true),
                    FormDesign = table.Column<string>(type: "text", nullable: true),
                    FormData = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedByNameSurname = table.Column<string>(type: "text", nullable: true),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormInstance_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormInstance_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormInstance_WorkflowHead_WorkflowHeadId",
                        column: x => x.WorkflowHeadId,
                        principalTable: "WorkflowHead",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 27, 14, 45, 23, 408, DateTimeKind.Utc).AddTicks(3730));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 27, 14, 45, 23, 408, DateTimeKind.Utc).AddTicks(2910));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 27, 14, 45, 23, 408, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.CreateIndex(
                name: "IX_FormInstance_FormId",
                table: "FormInstance",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstance_MainClientId_WorkflowHeadId",
                table: "FormInstance",
                columns: new[] { "MainClientId", "WorkflowHeadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormInstance_WorkflowHeadId",
                table: "FormInstance",
                column: "WorkflowHeadId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormInstance");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 34, 47, 13, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 34, 47, 13, DateTimeKind.Utc).AddTicks(250));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 34, 47, 13, DateTimeKind.Utc).AddTicks(1030));
        }
    }
}
