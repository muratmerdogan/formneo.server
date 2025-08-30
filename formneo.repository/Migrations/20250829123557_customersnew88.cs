using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class customersnew88 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContractEnd",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContractNo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DefaultNotificationEmail",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EInvoice",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmailPrimary",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LinkedinUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PreferredContact",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RichNote",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SectorDetailsJson",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TaxExemptionCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TermDays",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UtsInstitutionNumber",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Customers",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "ContractStart",
                table: "Customers",
                newName: "NextActivityDate");

            migrationBuilder.AlterColumn<string>(
                name: "LogoFilePath",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LifecycleStage",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Bulk",
                table: "CustomerEmails",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerEmails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomerEmails",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "CustomerEmails",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Notify",
                table: "CustomerEmails",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomerAddresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBilling",
                table: "CustomerAddresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShipping",
                table: "CustomerAddresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CustomerNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false),
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
                    table.PrimaryKey("PK_CustomerNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerNotes_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerNotes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerNotes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerNotes_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerPhones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false),
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
                    table.PrimaryKey("PK_CustomerPhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPhones_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerPhones_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerPhones_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerPhones_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 35, 54, 268, DateTimeKind.Utc).AddTicks(7668));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 35, 54, 268, DateTimeKind.Utc).AddTicks(2432));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 29, 12, 35, 54, 268, DateTimeKind.Utc).AddTicks(8103));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_CompanyId",
                table: "CustomerNotes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_CustomerId",
                table: "CustomerNotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_MainClientId",
                table: "CustomerNotes",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_PlantId",
                table: "CustomerNotes",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhones_CompanyId",
                table: "CustomerPhones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhones_CustomerId",
                table: "CustomerPhones",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhones_MainClientId",
                table: "CustomerPhones",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhones_PlantId",
                table: "CustomerPhones",
                column: "PlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerNotes");

            migrationBuilder.DropTable(
                name: "CustomerPhones");

            migrationBuilder.DropColumn(
                name: "LifecycleStage",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Bulk",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "Notify",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "IsBilling",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "IsShipping",
                table: "CustomerAddresses");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Customers",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "NextActivityDate",
                table: "Customers",
                newName: "ContractStart");

            migrationBuilder.AlterColumn<string>(
                name: "LogoFilePath",
                table: "Customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionCode",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEnd",
                table: "Customers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNo",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Customers",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultNotificationEmail",
                table: "Customers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Customers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EInvoice",
                table: "Customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmailPrimary",
                table: "Customers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinUrl",
                table: "Customers",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Customers",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Customers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferredContact",
                table: "Customers",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RichNote",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectorDetailsJson",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxExemptionCode",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TermDays",
                table: "Customers",
                type: "integer",
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
        }
    }
}
