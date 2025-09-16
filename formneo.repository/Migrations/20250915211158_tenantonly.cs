using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class tenantonly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTenantOnly",
                table: "Menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerTags",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerSectors",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Customers",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerPhones",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerOfficials",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerNotes",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerEmails",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerDocuments",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerCustomFields",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerAddresses",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 15, 21, 11, 55, 794, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 15, 21, 11, 55, 793, DateTimeKind.Utc).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 15, 21, 11, 55, 794, DateTimeKind.Utc).AddTicks(721));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTenantOnly",
                table: "Menus");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerTags",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerSectors",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Customers",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerPhones",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerOfficials",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerNotes",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerEmails",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerDocuments",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerCustomFields",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerAddresses",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldDefaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 14, 18, 23, 33, 406, DateTimeKind.Utc).AddTicks(2918));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 14, 18, 23, 33, 406, DateTimeKind.Utc).AddTicks(1319));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 14, 18, 23, 33, 406, DateTimeKind.Utc).AddTicks(3067));
        }
    }
}
