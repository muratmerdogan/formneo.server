using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class removecompny : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Plant_PlantId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ApproveItems_Companies_CompanyId",
                table: "ApproveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ApproveItems_Plant_PlantId",
                table: "ApproveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetJobCodeRequest_Companies_CompanyId",
                table: "BudgetJobCodeRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetJobCodeRequest_Plant_PlantId",
                table: "BudgetJobCodeRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetNormCodeRequest_Companies_CompanyId",
                table: "BudgetNormCodeRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetNormCodeRequest_Plant_PlantId",
                table: "BudgetNormCodeRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPeriod_Companies_CompanyId",
                table: "BudgetPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPeriod_Plant_PlantId",
                table: "BudgetPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPeriodUser_Companies_CompanyId",
                table: "BudgetPeriodUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPeriodUser_Plant_PlantId",
                table: "BudgetPeriodUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionRequest_Companies_CompanyId",
                table: "BudgetPromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotionRequest_Plant_PlantId",
                table: "BudgetPromotionRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Companies_CompanyId",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Plant_PlantId",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCustomFields_Companies_CompanyId",
                table: "CustomerCustomFields");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCustomFields_Plant_PlantId",
                table: "CustomerCustomFields");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDocuments_Companies_CompanyId",
                table: "CustomerDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDocuments_Plant_PlantId",
                table: "CustomerDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerEmails_Companies_CompanyId",
                table: "CustomerEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerEmails_Plant_PlantId",
                table: "CustomerEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerNotes_Companies_CompanyId",
                table: "CustomerNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerNotes_Plant_PlantId",
                table: "CustomerNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOfficials_Companies_CompanyId",
                table: "CustomerOfficials");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOfficials_Plant_PlantId",
                table: "CustomerOfficials");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPhones_Companies_CompanyId",
                table: "CustomerPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPhones_Plant_PlantId",
                table: "CustomerPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Companies_CompanyId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Plant_PlantId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSectors_Companies_CompanyId",
                table: "CustomerSectors");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSectors_Plant_PlantId",
                table: "CustomerSectors");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTags_Companies_CompanyId",
                table: "CustomerTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTags_Plant_PlantId",
                table: "CustomerTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Plant_PlantId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUsers_Companies_CompanyId",
                table: "DepartmentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUsers_Plant_PlantId",
                table: "DepartmentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Plant_PlantId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpSalary_Companies_CompanyId",
                table: "EmpSalary");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpSalary_Plant_PlantId",
                table: "EmpSalary");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Companies_CompanyId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Plant_PlantId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAssign_Companies_CompanyId",
                table: "FormAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAssign_Plant_PlantId",
                table: "FormAssign");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAuth_Companies_CompanyId",
                table: "FormAuth");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAuth_Plant_PlantId",
                table: "FormAuth");

            migrationBuilder.DropForeignKey(
                name: "FK_FormRuleEngine_Companies_CompanyId",
                table: "FormRuleEngine");

            migrationBuilder.DropForeignKey(
                name: "FK_FormRuleEngine_Plant_PlantId",
                table: "FormRuleEngine");

            migrationBuilder.DropForeignKey(
                name: "FK_FormRuntime_Companies_CompanyId",
                table: "FormRuntime");

            migrationBuilder.DropForeignKey(
                name: "FK_FormRuntime_Plant_PlantId",
                table: "FormRuntime");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Companies_CompanyId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Plant_PlantId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Kanban_Companies_CompanyId",
                table: "Kanban");

            migrationBuilder.DropForeignKey(
                name: "FK_Kanban_Plant_PlantId",
                table: "Kanban");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Companies_CompanyId",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Plant_PlantId",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Companies_CompanyId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Plant_PlantId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_PCTrack_Companies_CompanyId",
                table: "PCTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_PCTrack_Plant_PlantId",
                table: "PCTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Companies_CompanyId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Plant_PlantId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCategories_Companies_CompanyId",
                table: "ProjectCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCategories_Plant_PlantId",
                table: "ProjectCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Plant_PlantId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Companies_CompanyId",
                table: "ProjectTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Plant_PlantId",
                table: "ProjectTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteLines_Companies_CompanyId",
                table: "QuoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteLines_Plant_PlantId",
                table: "QuoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Companies_CompanyId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Plant_PlantId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Companies_CompanyId",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Plant_PlantId",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDays_Companies_CompanyId",
                table: "SpecialDays");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialDays_Plant_PlantId",
                table: "SpecialDays");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketApprove_Companies_CompanyId",
                table: "TicketApprove");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketApprove_Plant_PlantId",
                table: "TicketApprove");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketAssigne_Companies_CompanyId",
                table: "TicketAssigne");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketAssigne_Plant_PlantId",
                table: "TicketAssigne");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketComment_Companies_CompanyId",
                table: "TicketComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketComment_Plant_PlantId",
                table: "TicketComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDepartment_Companies_CompanyId",
                table: "TicketDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDepartment_Plant_PlantId",
                table: "TicketDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketFile_Companies_CompanyId",
                table: "TicketFile");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketFile_Plant_PlantId",
                table: "TicketFile");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketNotifications_Companies_CompanyId",
                table: "TicketNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketNotifications_Plant_PlantId",
                table: "TicketNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketProjects_Companies_CompanyId",
                table: "TicketProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketProjects_Plant_PlantId",
                table: "TicketProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRuleEngine_Companies_CompanyId",
                table: "TicketRuleEngine");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRuleEngine_Plant_PlantId",
                table: "TicketRuleEngine");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Companies_CompanyId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Plant_PlantId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTeam_Companies_CompanyId",
                table: "TicketTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTeam_Plant_PlantId",
                table: "TicketTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTeamUserApp_Companies_CompanyId",
                table: "TicketTeamUserApp");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTeamUserApp_Plant_PlantId",
                table: "TicketTeamUserApp");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCalendar_Companies_CompanyId",
                table: "UserCalendar");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCalendar_Plant_PlantId",
                table: "UserCalendar");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompany_Companies_CompanyId",
                table: "WorkCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompany_Plant_PlantId",
                table: "WorkCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanySystemInfo_Companies_CompanyId",
                table: "WorkCompanySystemInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanySystemInfo_Plant_PlantId",
                table: "WorkCompanySystemInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanyTicketMatris_Companies_CompanyId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCompanyTicketMatris_Plant_PlantId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowDefination_Companies_CompanyId",
                table: "WorkFlowDefination");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowDefination_Plant_PlantId",
                table: "WorkFlowDefination");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowHead_Companies_CompanyId",
                table: "WorkflowHead");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowHead_Plant_PlantId",
                table: "WorkflowHead");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowItem_Companies_CompanyId",
                table: "WorkflowItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowItem_Plant_PlantId",
                table: "WorkflowItem");

            migrationBuilder.DropTable(
                name: "BudgetAdminUser");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowItem_CompanyId",
                table: "WorkflowItem");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowItem_PlantId",
                table: "WorkflowItem");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowHead_CompanyId",
                table: "WorkflowHead");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowHead_PlantId",
                table: "WorkflowHead");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowDefination_CompanyId",
                table: "WorkFlowDefination");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowDefination_PlantId",
                table: "WorkFlowDefination");

            migrationBuilder.DropIndex(
                name: "IX_WorkCompanyTicketMatris_CompanyId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropIndex(
                name: "IX_WorkCompanyTicketMatris_PlantId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropIndex(
                name: "IX_WorkCompanySystemInfo_CompanyId",
                table: "WorkCompanySystemInfo");

            migrationBuilder.DropIndex(
                name: "IX_WorkCompanySystemInfo_PlantId",
                table: "WorkCompanySystemInfo");

            migrationBuilder.DropIndex(
                name: "IX_WorkCompany_CompanyId",
                table: "WorkCompany");

            migrationBuilder.DropIndex(
                name: "IX_WorkCompany_PlantId",
                table: "WorkCompany");

            migrationBuilder.DropIndex(
                name: "IX_UserCalendar_CompanyId",
                table: "UserCalendar");

            migrationBuilder.DropIndex(
                name: "IX_UserCalendar_PlantId",
                table: "UserCalendar");

            migrationBuilder.DropIndex(
                name: "IX_TicketTeamUserApp_CompanyId",
                table: "TicketTeamUserApp");

            migrationBuilder.DropIndex(
                name: "IX_TicketTeamUserApp_PlantId",
                table: "TicketTeamUserApp");

            migrationBuilder.DropIndex(
                name: "IX_TicketTeam_CompanyId",
                table: "TicketTeam");

            migrationBuilder.DropIndex(
                name: "IX_TicketTeam_PlantId",
                table: "TicketTeam");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CompanyId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PlantId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_TicketRuleEngine_CompanyId",
                table: "TicketRuleEngine");

            migrationBuilder.DropIndex(
                name: "IX_TicketRuleEngine_PlantId",
                table: "TicketRuleEngine");

            migrationBuilder.DropIndex(
                name: "IX_TicketProjects_CompanyId",
                table: "TicketProjects");

            migrationBuilder.DropIndex(
                name: "IX_TicketProjects_PlantId",
                table: "TicketProjects");

            migrationBuilder.DropIndex(
                name: "IX_TicketNotifications_CompanyId",
                table: "TicketNotifications");

            migrationBuilder.DropIndex(
                name: "IX_TicketNotifications_PlantId",
                table: "TicketNotifications");

            migrationBuilder.DropIndex(
                name: "IX_TicketFile_CompanyId",
                table: "TicketFile");

            migrationBuilder.DropIndex(
                name: "IX_TicketFile_PlantId",
                table: "TicketFile");

            migrationBuilder.DropIndex(
                name: "IX_TicketDepartment_CompanyId",
                table: "TicketDepartment");

            migrationBuilder.DropIndex(
                name: "IX_TicketDepartment_PlantId",
                table: "TicketDepartment");

            migrationBuilder.DropIndex(
                name: "IX_TicketComment_CompanyId",
                table: "TicketComment");

            migrationBuilder.DropIndex(
                name: "IX_TicketComment_PlantId",
                table: "TicketComment");

            migrationBuilder.DropIndex(
                name: "IX_TicketAssigne_CompanyId",
                table: "TicketAssigne");

            migrationBuilder.DropIndex(
                name: "IX_TicketAssigne_PlantId",
                table: "TicketAssigne");

            migrationBuilder.DropIndex(
                name: "IX_TicketApprove_CompanyId",
                table: "TicketApprove");

            migrationBuilder.DropIndex(
                name: "IX_TicketApprove_PlantId",
                table: "TicketApprove");

            migrationBuilder.DropIndex(
                name: "IX_SpecialDays_CompanyId",
                table: "SpecialDays");

            migrationBuilder.DropIndex(
                name: "IX_SpecialDays_PlantId",
                table: "SpecialDays");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_CompanyId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_PlantId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_CompanyId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_PlantId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_QuoteLines_CompanyId",
                table: "QuoteLines");

            migrationBuilder.DropIndex(
                name: "IX_QuoteLines_PlantId",
                table: "QuoteLines");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_CompanyId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_PlantId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_PlantId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectCategories_CompanyId",
                table: "ProjectCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProjectCategories_PlantId",
                table: "ProjectCategories");

            migrationBuilder.DropIndex(
                name: "IX_Positions_CompanyId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_PlantId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_PCTrack_CompanyId",
                table: "PCTrack");

            migrationBuilder.DropIndex(
                name: "IX_PCTrack_PlantId",
                table: "PCTrack");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_CompanyId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_PlantId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CompanyId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_PlantId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Kanban_CompanyId",
                table: "Kanban");

            migrationBuilder.DropIndex(
                name: "IX_Kanban_PlantId",
                table: "Kanban");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_CompanyId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_PlantId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_FormRuntime_CompanyId",
                table: "FormRuntime");

            migrationBuilder.DropIndex(
                name: "IX_FormRuntime_PlantId",
                table: "FormRuntime");

            migrationBuilder.DropIndex(
                name: "IX_FormRuleEngine_CompanyId",
                table: "FormRuleEngine");

            migrationBuilder.DropIndex(
                name: "IX_FormRuleEngine_PlantId",
                table: "FormRuleEngine");

            migrationBuilder.DropIndex(
                name: "IX_FormAuth_CompanyId",
                table: "FormAuth");

            migrationBuilder.DropIndex(
                name: "IX_FormAuth_PlantId",
                table: "FormAuth");

            migrationBuilder.DropIndex(
                name: "IX_FormAssign_CompanyId",
                table: "FormAssign");

            migrationBuilder.DropIndex(
                name: "IX_FormAssign_PlantId",
                table: "FormAssign");

            migrationBuilder.DropIndex(
                name: "IX_Form_CompanyId",
                table: "Form");

            migrationBuilder.DropIndex(
                name: "IX_Form_PlantId",
                table: "Form");

            migrationBuilder.DropIndex(
                name: "IX_EmpSalary_CompanyId",
                table: "EmpSalary");

            migrationBuilder.DropIndex(
                name: "IX_EmpSalary_PlantId",
                table: "EmpSalary");

            migrationBuilder.DropIndex(
                name: "IX_Employee_CompanyId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_PlantId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentUsers_CompanyId",
                table: "DepartmentUsers");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentUsers_PlantId",
                table: "DepartmentUsers");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_PlantId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTags_CompanyId",
                table: "CustomerTags");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTags_PlantId",
                table: "CustomerTags");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSectors_CompanyId",
                table: "CustomerSectors");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSectors_PlantId",
                table: "CustomerSectors");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CompanyId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PlantId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPhones_CompanyId",
                table: "CustomerPhones");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPhones_PlantId",
                table: "CustomerPhones");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOfficials_CompanyId",
                table: "CustomerOfficials");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOfficials_PlantId",
                table: "CustomerOfficials");

            migrationBuilder.DropIndex(
                name: "IX_CustomerNotes_CompanyId",
                table: "CustomerNotes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerNotes_PlantId",
                table: "CustomerNotes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerEmails_CompanyId",
                table: "CustomerEmails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerEmails_PlantId",
                table: "CustomerEmails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDocuments_CompanyId",
                table: "CustomerDocuments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDocuments_PlantId",
                table: "CustomerDocuments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCustomFields_CompanyId",
                table: "CustomerCustomFields");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCustomFields_PlantId",
                table: "CustomerCustomFields");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_CompanyId",
                table: "CustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_PlantId",
                table: "CustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionRequest_CompanyId",
                table: "BudgetPromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotionRequest_PlantId",
                table: "BudgetPromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPeriodUser_BudgetPeriodCode_UserName_requestType",
                table: "BudgetPeriodUser");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPeriodUser_CompanyId",
                table: "BudgetPeriodUser");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPeriodUser_PlantId",
                table: "BudgetPeriodUser");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPeriod_CompanyId",
                table: "BudgetPeriod");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPeriod_PlantId",
                table: "BudgetPeriod");

            migrationBuilder.DropIndex(
                name: "IX_BudgetNormCodeRequest_CompanyId",
                table: "BudgetNormCodeRequest");

            migrationBuilder.DropIndex(
                name: "IX_BudgetNormCodeRequest_PlantId",
                table: "BudgetNormCodeRequest");

            migrationBuilder.DropIndex(
                name: "IX_BudgetJobCodeRequest_CompanyId",
                table: "BudgetJobCodeRequest");

            migrationBuilder.DropIndex(
                name: "IX_BudgetJobCodeRequest_PlantId",
                table: "BudgetJobCodeRequest");

            migrationBuilder.DropIndex(
                name: "IX_ApproveItems_CompanyId",
                table: "ApproveItems");

            migrationBuilder.DropIndex(
                name: "IX_ApproveItems_PlantId",
                table: "ApproveItems");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CompanyId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_PlantId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkflowItem");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "WorkflowItem");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkflowHead");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "WorkflowHead");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkFlowDefination");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "WorkFlowDefination");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "WorkCompanyTicketMatris");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkCompanySystemInfo");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "WorkCompanySystemInfo");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkCompany");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "WorkCompany");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserCalendar");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "UserCalendar");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketTeamUserApp");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketTeamUserApp");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketTeam");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketTeam");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketRuleEngine");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketRuleEngine");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketProjects");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketProjects");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketNotifications");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketNotifications");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketFile");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketFile");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketDepartment");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketDepartment");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketAssigne");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketAssigne");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TicketApprove");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TicketApprove");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "SpecialDays");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "SpecialDays");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "QuoteLines");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "QuoteLines");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ProjectCategories");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "ProjectCategories");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PCTrack");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "PCTrack");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Kanban");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Kanban");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FormRuntime");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "FormRuntime");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FormRuleEngine");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "FormRuleEngine");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FormAuth");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "FormAuth");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FormAssign");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "FormAssign");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "EmpSalary");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "EmpSalary");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DepartmentUsers");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "DepartmentUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerTags");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerTags");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerSectors");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerSectors");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerPhones");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerPhones");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerOfficials");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerOfficials");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerNotes");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerNotes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerEmails");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerCustomFields");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerCustomFields");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BudgetPromotionRequest");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "BudgetPromotionRequest");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BudgetPeriodUser");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "BudgetPeriodUser");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BudgetPeriod");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "BudgetPeriod");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BudgetNormCodeRequest");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "BudgetNormCodeRequest");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BudgetJobCodeRequest");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "BudgetJobCodeRequest");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ApproveItems");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "ApproveItems");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "MainClientId",
                table: "TicketAssigne",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "MainClientId",
                table: "TicketApprove",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
                value: new DateTime(2025, 9, 25, 21, 37, 44, 911, DateTimeKind.Utc).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 21, 37, 44, 911, DateTimeKind.Utc).AddTicks(9090));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 21, 37, 44, 912, DateTimeKind.Utc).AddTicks(40));

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_BudgetPeriodCode",
                table: "BudgetPeriodUser",
                column: "BudgetPeriodCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BudgetPeriodUser_BudgetPeriodCode",
                table: "BudgetPeriodUser");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "WorkflowItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "WorkflowItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "WorkflowHead",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "WorkflowHead",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "WorkFlowDefination",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "WorkFlowDefination",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "WorkCompanyTicketMatris",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "WorkCompanyTicketMatris",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "WorkCompanySystemInfo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "WorkCompanySystemInfo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "WorkCompany",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "WorkCompany",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "UserCalendar",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "UserCalendar",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketTeamUserApp",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketTeamUserApp",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketTeam",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketTeam",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketRuleEngine",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketRuleEngine",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketProjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketProjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketNotifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketNotifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketFile",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketFile",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketDepartment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketDepartment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketComment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketComment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "MainClientId",
                table: "TicketAssigne",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketAssigne",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketAssigne",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "MainClientId",
                table: "TicketApprove",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "TicketApprove",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "TicketApprove",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "SpecialDays",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "SpecialDays",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Reminders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Reminders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Quotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Quotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "QuoteLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "QuoteLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "ProjectTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "ProjectTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "ProjectCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "ProjectCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Positions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Positions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "PCTrack",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "PCTrack",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Opportunities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Opportunities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Meetings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Meetings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Kanban",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Kanban",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Inventory",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Inventory",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "FormRuntime",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "FormRuntime",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "FormRuleEngine",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "FormRuleEngine",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "FormAuth",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "FormAuth",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "FormAssign",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "FormAssign",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Form",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Form",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "EmpSalary",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "EmpSalary",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Employee",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Employee",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "DepartmentUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "DepartmentUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Departments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Departments",
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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerTags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerTags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerSectors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerSectors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerPhones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerPhones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerOfficials",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerOfficials",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerNotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerNotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerEmails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerEmails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CustomerCustomFields",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerCustomFields",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                table: "CustomerAddresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "CustomerAddresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "BudgetPromotionRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "BudgetPromotionRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "BudgetPeriodUser",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "BudgetPeriodUser",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "BudgetPeriod",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "BudgetPeriod",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "BudgetNormCodeRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "BudgetNormCodeRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "BudgetJobCodeRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "BudgetJobCodeRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "ApproveItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "ApproveItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Activities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlantId",
                table: "Activities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BudgetAdminUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    MainClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminLevel = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    IsDoProxy = table.Column<bool>(type: "boolean", nullable: false),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    ProxyUser = table.Column<string>(type: "text", nullable: false),
                    UniqNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetAdminUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetAdminUser_Clients_MainClientId",
                        column: x => x.MainClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetAdminUser_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetAdminUser_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 21, 17, 38, 2, 312, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 21, 17, 38, 2, 312, DateTimeKind.Utc).AddTicks(8660));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 9, 21, 17, 38, 2, 312, DateTimeKind.Utc).AddTicks(9440));

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItem_CompanyId",
                table: "WorkflowItem",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItem_PlantId",
                table: "WorkflowItem",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowHead_CompanyId",
                table: "WorkflowHead",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowHead_PlantId",
                table: "WorkflowHead",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDefination_CompanyId",
                table: "WorkFlowDefination",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDefination_PlantId",
                table: "WorkFlowDefination",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanyTicketMatris_CompanyId",
                table: "WorkCompanyTicketMatris",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanyTicketMatris_PlantId",
                table: "WorkCompanyTicketMatris",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanySystemInfo_CompanyId",
                table: "WorkCompanySystemInfo",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompanySystemInfo_PlantId",
                table: "WorkCompanySystemInfo",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompany_CompanyId",
                table: "WorkCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCompany_PlantId",
                table: "WorkCompany",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendar_CompanyId",
                table: "UserCalendar",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCalendar_PlantId",
                table: "UserCalendar",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeamUserApp_CompanyId",
                table: "TicketTeamUserApp",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeamUserApp_PlantId",
                table: "TicketTeamUserApp",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_CompanyId",
                table: "TicketTeam",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTeam_PlantId",
                table: "TicketTeam",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CompanyId",
                table: "Tickets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PlantId",
                table: "Tickets",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRuleEngine_CompanyId",
                table: "TicketRuleEngine",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRuleEngine_PlantId",
                table: "TicketRuleEngine",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_CompanyId",
                table: "TicketProjects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProjects_PlantId",
                table: "TicketProjects",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotifications_CompanyId",
                table: "TicketNotifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNotifications_PlantId",
                table: "TicketNotifications",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFile_CompanyId",
                table: "TicketFile",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketFile_PlantId",
                table: "TicketFile",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_CompanyId",
                table: "TicketDepartment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDepartment_PlantId",
                table: "TicketDepartment",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_CompanyId",
                table: "TicketComment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_PlantId",
                table: "TicketComment",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_CompanyId",
                table: "TicketAssigne",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssigne_PlantId",
                table: "TicketAssigne",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketApprove_CompanyId",
                table: "TicketApprove",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketApprove_PlantId",
                table: "TicketApprove",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDays_CompanyId",
                table: "SpecialDays",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDays_PlantId",
                table: "SpecialDays",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_CompanyId",
                table: "Reminders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_PlantId",
                table: "Reminders",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_CompanyId",
                table: "Quotes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_PlantId",
                table: "Quotes",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteLines_CompanyId",
                table: "QuoteLines",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteLines_PlantId",
                table: "QuoteLines",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_CompanyId",
                table: "ProjectTasks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_PlantId",
                table: "ProjectTasks",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PlantId",
                table: "Projects",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategories_CompanyId",
                table: "ProjectCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategories_PlantId",
                table: "ProjectCategories",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CompanyId",
                table: "Positions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PlantId",
                table: "Positions",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PCTrack_CompanyId",
                table: "PCTrack",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PCTrack_PlantId",
                table: "PCTrack",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_CompanyId",
                table: "Opportunities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_PlantId",
                table: "Opportunities",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CompanyId",
                table: "Meetings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_PlantId",
                table: "Meetings",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanban_CompanyId",
                table: "Kanban",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanban_PlantId",
                table: "Kanban",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CompanyId",
                table: "Inventory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PlantId",
                table: "Inventory",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuntime_CompanyId",
                table: "FormRuntime",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuntime_PlantId",
                table: "FormRuntime",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuleEngine_CompanyId",
                table: "FormRuleEngine",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormRuleEngine_PlantId",
                table: "FormRuleEngine",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAuth_CompanyId",
                table: "FormAuth",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAuth_PlantId",
                table: "FormAuth",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAssign_CompanyId",
                table: "FormAssign",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAssign_PlantId",
                table: "FormAssign",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_CompanyId",
                table: "Form",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_PlantId",
                table: "Form",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpSalary_CompanyId",
                table: "EmpSalary",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpSalary_PlantId",
                table: "EmpSalary",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompanyId",
                table: "Employee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PlantId",
                table: "Employee",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_CompanyId",
                table: "DepartmentUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_PlantId",
                table: "DepartmentUsers",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_PlantId",
                table: "Departments",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTags_CompanyId",
                table: "CustomerTags",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTags_PlantId",
                table: "CustomerTags",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSectors_CompanyId",
                table: "CustomerSectors",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSectors_PlantId",
                table: "CustomerSectors",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyId",
                table: "Customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PlantId",
                table: "Customers",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhones_CompanyId",
                table: "CustomerPhones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhones_PlantId",
                table: "CustomerPhones",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOfficials_CompanyId",
                table: "CustomerOfficials",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOfficials_PlantId",
                table: "CustomerOfficials",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_CompanyId",
                table: "CustomerNotes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNotes_PlantId",
                table: "CustomerNotes",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmails_CompanyId",
                table: "CustomerEmails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEmails_PlantId",
                table: "CustomerEmails",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_CompanyId",
                table: "CustomerDocuments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_PlantId",
                table: "CustomerDocuments",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCustomFields_CompanyId",
                table: "CustomerCustomFields",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCustomFields_PlantId",
                table: "CustomerCustomFields",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CompanyId",
                table: "CustomerAddresses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_PlantId",
                table: "CustomerAddresses",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionRequest_CompanyId",
                table: "BudgetPromotionRequest",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionRequest_PlantId",
                table: "BudgetPromotionRequest",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_BudgetPeriodCode_UserName_requestType",
                table: "BudgetPeriodUser",
                columns: new[] { "BudgetPeriodCode", "UserName", "requestType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_CompanyId",
                table: "BudgetPeriodUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriodUser_PlantId",
                table: "BudgetPeriodUser",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriod_CompanyId",
                table: "BudgetPeriod",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPeriod_PlantId",
                table: "BudgetPeriod",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetNormCodeRequest_CompanyId",
                table: "BudgetNormCodeRequest",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetNormCodeRequest_PlantId",
                table: "BudgetNormCodeRequest",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJobCodeRequest_CompanyId",
                table: "BudgetJobCodeRequest",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJobCodeRequest_PlantId",
                table: "BudgetJobCodeRequest",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveItems_CompanyId",
                table: "ApproveItems",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveItems_PlantId",
                table: "ApproveItems",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CompanyId",
                table: "Activities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_PlantId",
                table: "Activities",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAdminUser_CompanyId",
                table: "BudgetAdminUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAdminUser_MainClientId",
                table: "BudgetAdminUser",
                column: "MainClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAdminUser_PlantId",
                table: "BudgetAdminUser",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Plant_PlantId",
                table: "Activities",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApproveItems_Companies_CompanyId",
                table: "ApproveItems",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApproveItems_Plant_PlantId",
                table: "ApproveItems",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetJobCodeRequest_Companies_CompanyId",
                table: "BudgetJobCodeRequest",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetJobCodeRequest_Plant_PlantId",
                table: "BudgetJobCodeRequest",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetNormCodeRequest_Companies_CompanyId",
                table: "BudgetNormCodeRequest",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetNormCodeRequest_Plant_PlantId",
                table: "BudgetNormCodeRequest",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPeriod_Companies_CompanyId",
                table: "BudgetPeriod",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPeriod_Plant_PlantId",
                table: "BudgetPeriod",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPeriodUser_Companies_CompanyId",
                table: "BudgetPeriodUser",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPeriodUser_Plant_PlantId",
                table: "BudgetPeriodUser",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionRequest_Companies_CompanyId",
                table: "BudgetPromotionRequest",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotionRequest_Plant_PlantId",
                table: "BudgetPromotionRequest",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Companies_CompanyId",
                table: "CustomerAddresses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Plant_PlantId",
                table: "CustomerAddresses",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCustomFields_Companies_CompanyId",
                table: "CustomerCustomFields",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCustomFields_Plant_PlantId",
                table: "CustomerCustomFields",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDocuments_Companies_CompanyId",
                table: "CustomerDocuments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDocuments_Plant_PlantId",
                table: "CustomerDocuments",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerEmails_Companies_CompanyId",
                table: "CustomerEmails",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerEmails_Plant_PlantId",
                table: "CustomerEmails",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerNotes_Companies_CompanyId",
                table: "CustomerNotes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerNotes_Plant_PlantId",
                table: "CustomerNotes",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOfficials_Companies_CompanyId",
                table: "CustomerOfficials",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOfficials_Plant_PlantId",
                table: "CustomerOfficials",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPhones_Companies_CompanyId",
                table: "CustomerPhones",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPhones_Plant_PlantId",
                table: "CustomerPhones",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Companies_CompanyId",
                table: "Customers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Plant_PlantId",
                table: "Customers",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSectors_Companies_CompanyId",
                table: "CustomerSectors",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSectors_Plant_PlantId",
                table: "CustomerSectors",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTags_Companies_CompanyId",
                table: "CustomerTags",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTags_Plant_PlantId",
                table: "CustomerTags",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Plant_PlantId",
                table: "Departments",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUsers_Companies_CompanyId",
                table: "DepartmentUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUsers_Plant_PlantId",
                table: "DepartmentUsers",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                table: "Employee",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Plant_PlantId",
                table: "Employee",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpSalary_Companies_CompanyId",
                table: "EmpSalary",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpSalary_Plant_PlantId",
                table: "EmpSalary",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Form_Companies_CompanyId",
                table: "Form",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Form_Plant_PlantId",
                table: "Form",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAssign_Companies_CompanyId",
                table: "FormAssign",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAssign_Plant_PlantId",
                table: "FormAssign",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAuth_Companies_CompanyId",
                table: "FormAuth",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAuth_Plant_PlantId",
                table: "FormAuth",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormRuleEngine_Companies_CompanyId",
                table: "FormRuleEngine",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormRuleEngine_Plant_PlantId",
                table: "FormRuleEngine",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormRuntime_Companies_CompanyId",
                table: "FormRuntime",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormRuntime_Plant_PlantId",
                table: "FormRuntime",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Companies_CompanyId",
                table: "Inventory",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Plant_PlantId",
                table: "Inventory",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kanban_Companies_CompanyId",
                table: "Kanban",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kanban_Plant_PlantId",
                table: "Kanban",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Companies_CompanyId",
                table: "Meetings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Plant_PlantId",
                table: "Meetings",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Companies_CompanyId",
                table: "Opportunities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Plant_PlantId",
                table: "Opportunities",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PCTrack_Companies_CompanyId",
                table: "PCTrack",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PCTrack_Plant_PlantId",
                table: "PCTrack",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Companies_CompanyId",
                table: "Positions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Plant_PlantId",
                table: "Positions",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCategories_Companies_CompanyId",
                table: "ProjectCategories",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCategories_Plant_PlantId",
                table: "ProjectCategories",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Plant_PlantId",
                table: "Projects",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Companies_CompanyId",
                table: "ProjectTasks",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Plant_PlantId",
                table: "ProjectTasks",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteLines_Companies_CompanyId",
                table: "QuoteLines",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteLines_Plant_PlantId",
                table: "QuoteLines",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Companies_CompanyId",
                table: "Quotes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Plant_PlantId",
                table: "Quotes",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Companies_CompanyId",
                table: "Reminders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Plant_PlantId",
                table: "Reminders",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialDays_Companies_CompanyId",
                table: "SpecialDays",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialDays_Plant_PlantId",
                table: "SpecialDays",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketApprove_Companies_CompanyId",
                table: "TicketApprove",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketApprove_Plant_PlantId",
                table: "TicketApprove",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAssigne_Companies_CompanyId",
                table: "TicketAssigne",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAssigne_Plant_PlantId",
                table: "TicketAssigne",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComment_Companies_CompanyId",
                table: "TicketComment",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComment_Plant_PlantId",
                table: "TicketComment",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDepartment_Companies_CompanyId",
                table: "TicketDepartment",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDepartment_Plant_PlantId",
                table: "TicketDepartment",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketFile_Companies_CompanyId",
                table: "TicketFile",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketFile_Plant_PlantId",
                table: "TicketFile",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketNotifications_Companies_CompanyId",
                table: "TicketNotifications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketNotifications_Plant_PlantId",
                table: "TicketNotifications",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketProjects_Companies_CompanyId",
                table: "TicketProjects",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketProjects_Plant_PlantId",
                table: "TicketProjects",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRuleEngine_Companies_CompanyId",
                table: "TicketRuleEngine",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRuleEngine_Plant_PlantId",
                table: "TicketRuleEngine",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Companies_CompanyId",
                table: "Tickets",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Plant_PlantId",
                table: "Tickets",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTeam_Companies_CompanyId",
                table: "TicketTeam",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTeam_Plant_PlantId",
                table: "TicketTeam",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTeamUserApp_Companies_CompanyId",
                table: "TicketTeamUserApp",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTeamUserApp_Plant_PlantId",
                table: "TicketTeamUserApp",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCalendar_Companies_CompanyId",
                table: "UserCalendar",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCalendar_Plant_PlantId",
                table: "UserCalendar",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCompany_Companies_CompanyId",
                table: "WorkCompany",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCompany_Plant_PlantId",
                table: "WorkCompany",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCompanySystemInfo_Companies_CompanyId",
                table: "WorkCompanySystemInfo",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCompanySystemInfo_Plant_PlantId",
                table: "WorkCompanySystemInfo",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCompanyTicketMatris_Companies_CompanyId",
                table: "WorkCompanyTicketMatris",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCompanyTicketMatris_Plant_PlantId",
                table: "WorkCompanyTicketMatris",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowDefination_Companies_CompanyId",
                table: "WorkFlowDefination",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowDefination_Plant_PlantId",
                table: "WorkFlowDefination",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowHead_Companies_CompanyId",
                table: "WorkflowHead",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowHead_Plant_PlantId",
                table: "WorkflowHead",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowItem_Companies_CompanyId",
                table: "WorkflowItem",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowItem_Plant_PlantId",
                table: "WorkflowItem",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");
        }
    }
}
