using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class removepermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 43, 21, 894, DateTimeKind.Utc).AddTicks(5724));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 43, 21, 893, DateTimeKind.Utc).AddTicks(9835));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 43, 21, 894, DateTimeKind.Utc).AddTicks(5964));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 31, 42, 744, DateTimeKind.Utc).AddTicks(5431));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 31, 42, 744, DateTimeKind.Utc).AddTicks(3702));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 31, 42, 744, DateTimeKind.Utc).AddTicks(5593));
        }
    }
}
