using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class rowversionnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowHead");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkFlowDefination");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkCompanySystemInfo");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkCompany");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "UserCalendar");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketTeamUserApp");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketTeam");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketRuleEngine");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketProjects");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketNotifications");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketFile");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketDepartment");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketAssigne");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TicketApprove");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "QuoteLines");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ProjectCategories");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "PCTrack");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Kanban");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormRuntime");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormRuleEngine");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormAuth");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormAssign");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "EmpSalary");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DepartmentUsers");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerTags");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerSectors");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerPhones");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerOfficials");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerNotes");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerCustomFields");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BudgetPromotionRequest");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BudgetPeriodUser");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BudgetPeriod");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BudgetNormCodeRequest");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BudgetJobCodeRequest");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApproveItems");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Activities");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Reminders",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Quotes",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "QuoteLines",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Opportunities",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Meetings",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerTags",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerSectors",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Customers",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerPhones",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerOfficials",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerNotes",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerEmails",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerDocuments",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerCustomFields",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "CustomerAddresses",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "Activities",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 26, 18, 52, 6, 853, DateTimeKind.Utc).AddTicks(9640));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 26, 18, 52, 6, 853, DateTimeKind.Utc).AddTicks(8820));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 26, 18, 52, 6, 853, DateTimeKind.Utc).AddTicks(9720));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "QuoteLines");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerTags");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerSectors");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerPhones");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerOfficials");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerNotes");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerCustomFields");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "Activities");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowItem",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowHead",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkFlowDefination",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkCompanyTicketMatris",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkCompanySystemInfo",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkCompany",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "UserCalendar",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketTeamUserApp",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketTeam",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Tickets",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketRuleEngine",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketProjects",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketNotifications",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketFile",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketDepartment",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketComment",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketAssigne",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TicketApprove",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Reminders",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Quotes",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "QuoteLines",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ProjectTasks",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Projects",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ProjectCategories",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Positions",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "PCTrack",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Opportunities",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Meetings",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Kanban",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Inventory",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormRuntime",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormRuleEngine",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormAuth",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormAssign",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Form",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "EmpSalary",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Employee",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DepartmentUsers",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Departments",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerTags",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerSectors",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Customers",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerPhones",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerOfficials",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerNotes",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerEmails",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerDocuments",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerCustomFields",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CustomerAddresses",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BudgetPromotionRequest",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BudgetPeriodUser",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BudgetPeriod",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BudgetNormCodeRequest",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BudgetJobCodeRequest",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApproveItems",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Activities",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 26, 16, 54, 0, 586, DateTimeKind.Utc).AddTicks(8640));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 26, 16, 54, 0, 586, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 26, 16, 54, 0, 586, DateTimeKind.Utc).AddTicks(8750));
        }
    }
}
