using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class AddFormIdToWorkflowHead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FormId",
                table: "WorkflowHead",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 12, 20, 26, 41, 780, DateTimeKind.Utc).AddTicks(4850));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 12, 20, 26, 41, 780, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 12, 20, 26, 41, 780, DateTimeKind.Utc).AddTicks(4930));

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowHead_FormId",
                table: "WorkflowHead",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowHead_Form_FormId",
                table: "WorkflowHead",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowHead_Form_FormId",
                table: "WorkflowHead");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowHead_FormId",
                table: "WorkflowHead");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "WorkflowHead");

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
        }
    }
}
