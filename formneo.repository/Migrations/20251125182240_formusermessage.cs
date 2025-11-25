using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class formusermessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormTaskMessage",
                table: "FormItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormUserMessage",
                table: "FormItems",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 22, 39, 525, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 22, 39, 525, DateTimeKind.Utc).AddTicks(4560));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 22, 39, 525, DateTimeKind.Utc).AddTicks(5400));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormTaskMessage",
                table: "FormItems");

            migrationBuilder.DropColumn(
                name: "FormUserMessage",
                table: "FormItems");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 12, 54, 419, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 12, 54, 419, DateTimeKind.Utc).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 18, 12, 54, 419, DateTimeKind.Utc).AddTicks(8470));
        }
    }
}
