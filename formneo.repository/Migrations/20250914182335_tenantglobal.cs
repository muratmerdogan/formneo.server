using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class tenantglobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRolesTenantMenu_Clients_MainClientId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRolesTenantMenu_Companies_CompanyId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRolesTenantMenu_Plant_PlantId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleTenants_Clients_MainClientId",
                table: "RoleTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleTenants_Companies_CompanyId",
                table: "RoleTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleTenants_Plant_PlantId",
                table: "RoleTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTenantRoles_Clients_MainClientId",
                table: "UserTenantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTenantRoles_Companies_CompanyId",
                table: "UserTenantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTenantRoles_Plant_PlantId",
                table: "UserTenantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTenants_Clients_MainClientId",
                table: "UserTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTenants_Companies_CompanyId",
                table: "UserTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTenants_Plant_PlantId",
                table: "UserTenants");

            migrationBuilder.DropIndex(
                name: "IX_UserTenants_CompanyId",
                table: "UserTenants");

            migrationBuilder.DropIndex(
                name: "IX_UserTenants_MainClientId",
                table: "UserTenants");

            migrationBuilder.DropIndex(
                name: "IX_UserTenants_PlantId",
                table: "UserTenants");

            migrationBuilder.DropIndex(
                name: "IX_UserTenantRoles_CompanyId",
                table: "UserTenantRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserTenantRoles_MainClientId",
                table: "UserTenantRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserTenantRoles_PlantId",
                table: "UserTenantRoles");

            migrationBuilder.DropIndex(
                name: "IX_RoleTenants_CompanyId",
                table: "RoleTenants");

            migrationBuilder.DropIndex(
                name: "IX_RoleTenants_MainClientId",
                table: "RoleTenants");

            migrationBuilder.DropIndex(
                name: "IX_RoleTenants_PlantId",
                table: "RoleTenants");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRolesTenantMenu_CompanyId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRolesTenantMenu_MainClientId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRolesTenantMenu_PlantId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "MainClientId",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "UserTenants");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserTenantRoles");

            migrationBuilder.DropColumn(
                name: "MainClientId",
                table: "UserTenantRoles");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "UserTenantRoles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "RoleTenants");

            migrationBuilder.DropColumn(
                name: "MainClientId",
                table: "RoleTenants");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "RoleTenants");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropColumn(
                name: "MainClientId",
                table: "AspNetRolesTenantMenu");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "AspNetRolesTenantMenu");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "UserTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MainClientId",
                table: "UserTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "UserTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "UserTenantRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MainClientId",
                table: "UserTenantRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "UserTenantRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "RoleTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MainClientId",
                table: "RoleTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "RoleTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AspNetRolesTenantMenu",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MainClientId",
                table: "AspNetRolesTenantMenu",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "AspNetRolesTenantMenu",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 14, 13, 23, 31, 596, DateTimeKind.Utc).AddTicks(3402));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 14, 13, 23, 31, 595, DateTimeKind.Utc).AddTicks(6038));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 14, 13, 23, 31, 596, DateTimeKind.Utc).AddTicks(3798));

            migrationBuilder.CreateIndex(
                name: "IX_UserTenants_CompanyId",
                table: "UserTenants",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenants_MainClientId",
                table: "UserTenants",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenants_PlantId",
                table: "UserTenants",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantRoles_CompanyId",
                table: "UserTenantRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantRoles_MainClientId",
                table: "UserTenantRoles",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantRoles_PlantId",
                table: "UserTenantRoles",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTenants_CompanyId",
                table: "RoleTenants",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTenants_MainClientId",
                table: "RoleTenants",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTenants_PlantId",
                table: "RoleTenants",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesTenantMenu_CompanyId",
                table: "AspNetRolesTenantMenu",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesTenantMenu_MainClientId",
                table: "AspNetRolesTenantMenu",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolesTenantMenu_PlantId",
                table: "AspNetRolesTenantMenu",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRolesTenantMenu_Clients_MainClientId",
                table: "AspNetRolesTenantMenu",
                column: "MainClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRolesTenantMenu_Companies_CompanyId",
                table: "AspNetRolesTenantMenu",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRolesTenantMenu_Plant_PlantId",
                table: "AspNetRolesTenantMenu",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleTenants_Clients_MainClientId",
                table: "RoleTenants",
                column: "MainClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleTenants_Companies_CompanyId",
                table: "RoleTenants",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleTenants_Plant_PlantId",
                table: "RoleTenants",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTenantRoles_Clients_MainClientId",
                table: "UserTenantRoles",
                column: "MainClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTenantRoles_Companies_CompanyId",
                table: "UserTenantRoles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTenantRoles_Plant_PlantId",
                table: "UserTenantRoles",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTenants_Clients_MainClientId",
                table: "UserTenants",
                column: "MainClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTenants_Companies_CompanyId",
                table: "UserTenants",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTenants_Plant_PlantId",
                table: "UserTenants",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");
        }
    }
}
