using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class yeni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DontApplyDefaultFilters",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasDepartmentPermission",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasOtherCompanyPermission",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasOtherDeptCalendarPerm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasTicketPermission",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PCname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "canEditTicket",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "mainManagerUserAppId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "manager1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "manager2",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "DontApplyDefaultFilters",
                table: "UserTenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDepartmentPermission",
                table: "UserTenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasOtherCompanyPermission",
                table: "UserTenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasOtherDeptCalendarPerm",
                table: "UserTenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTicketPermission",
                table: "UserTenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PCname",
                table: "UserTenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "canEditTicket",
                table: "UserTenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "mainManagerUserAppId",
                table: "UserTenants",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "manager1",
                table: "UserTenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "manager2",
                table: "UserTenants",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 12, 25, 33, 185, DateTimeKind.Utc).AddTicks(6617));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 12, 25, 33, 185, DateTimeKind.Utc).AddTicks(3535));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 12, 25, 33, 185, DateTimeKind.Utc).AddTicks(6894));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DontApplyDefaultFilters",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "HasDepartmentPermission",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "HasOtherCompanyPermission",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "HasOtherDeptCalendarPerm",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "HasTicketPermission",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "PCname",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "canEditTicket",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "mainManagerUserAppId",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "manager1",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "manager2",
                table: "UserTenants");

            migrationBuilder.AddColumn<bool>(
                name: "DontApplyDefaultFilters",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDepartmentPermission",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasOtherCompanyPermission",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasOtherDeptCalendarPerm",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTicketPermission",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PCname",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "canEditTicket",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "mainManagerUserAppId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "manager1",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "manager2",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 11, 2, 17, 727, DateTimeKind.Utc).AddTicks(8041));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 11, 2, 17, 727, DateTimeKind.Utc).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 11, 2, 17, 727, DateTimeKind.Utc).AddTicks(8180));
        }
    }
}
