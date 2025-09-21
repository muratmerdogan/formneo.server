using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.BudgetAdminUser;
using vesa.core.DTOs.Budget.JobCodeRequest;
using vesa.core.DTOs.Budget.NormCodeRequest;
using vesa.core.DTOs.Budget.PeriodUserDto;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.Clients;
using vesa.core.DTOs.Company;
using vesa.core.DTOs.DepartmentUserDto;
using vesa.core.DTOs.FormAssign;
using vesa.core.DTOs.FormAuth;
using vesa.core.DTOs.FormDatas;
using vesa.core.DTOs.Inventory;
using vesa.core.DTOs.Kanban;
using vesa.core.DTOs.Menu;
using vesa.core.DTOs.PCTrack;
using vesa.core.DTOs.Plants;
using vesa.core.DTOs.PositionsDtos;
using vesa.core.DTOs.ProjectDtos;
using vesa.core.DTOs.ProjectTasks;
using vesa.core.DTOs.TaskManagement;
using vesa.core.DTOs.UserTenants;
using vesa.core.DTOs.Ticket;
using vesa.core.DTOs.Ticket.TicketAssigne;
using vesa.core.DTOs.Ticket.TicketComment;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.Ticket.TicketNotifications;
using vesa.core.DTOs.Ticket.TicketRuleEngine;
using vesa.core.DTOs.Ticket.Tickets;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.DTOs.TicketProjects;
using vesa.core.GenericListDto;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;
using vesa.core.Models.Inventory;
using vesa.core.Models.NewFolder;
using vesa.core.Models.PCTracking;
using vesa.core.Models.TaskManagement;
using vesa.core.Models.Ticket;
using vesa.core.Models.CRM;
using vesa.core.DTOs.CRM;
using vesa.core.DTOs.Lookup;
using vesa.core.Models.Lookup;

namespace vesa.service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<EmpSalary, EmpSalaryDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Departments, DepartmentsUpdateDto>().ReverseMap();
            CreateMap<Departments, DepartmentsInsertDto>().ReverseMap();
            CreateMap<Departments, DepartmentsListDto>().ReverseMap();


            CreateMap<UserApp, UserAppDto>().ReverseMap();

            CreateMap<UserApp, CreateUserDto>().ReverseMap();
            CreateMap<UserApp, UpdateUserDto>().ReverseMap();
            CreateMap<WorkFlowDefination, WorkFlowDefinationDto>().ReverseMap();
            CreateMap<WorkFlowDefination, WorkFlowDefinationDto>().ReverseMap();
            CreateMap<ApproveItems, ApproveItemsDto>().ReverseMap();
            CreateMap<WorkflowHead, WorkFlowHeadDto>().ReverseMap();
            CreateMap<Form, FormDataListDto>().ReverseMap();
            CreateMap<FormRuleEngineDto, FormRuleEngine>().ReverseMap();
            CreateMap<FormRuntimeDto, FormRuntime>().ReverseMap();
            CreateMap<FormRuntimeDto, FormRuntimeSubmission>().ReverseMap();

            CreateMap<MainClient, MainClientInsertDto>().ReverseMap();
            CreateMap<MainClient, MainClientListDto>().ReverseMap();
            CreateMap<MainClient, MainClientUpdateDto>().ReverseMap();

            CreateMap<Company, CompanyListDto>().ReverseMap();
            CreateMap<Company, CompanyInsertDto>().ReverseMap();
            CreateMap<Company, CompanyUpdateDto>().ReverseMap();

            CreateMap<Plant, PlantListDto>().ReverseMap();
            CreateMap<Plant, PlantInsertDto>().ReverseMap();
            CreateMap<Plant, PlantUpdateDto>().ReverseMap();

            CreateMap<Form, FormDataInsertDto>().ReverseMap();
            CreateMap<Form, FormDataUpdateDto>().ReverseMap();
            CreateMap<Form, FormDataListDto>().ReverseMap();
            CreateMap<Form, FormDataGetByIdDto>().ReverseMap();

            CreateMap<Project, GetProjectListDto>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();

            CreateMap<Positions, UpdatePositionDto>().ReverseMap();
            CreateMap<Positions, CreatePositionDto>().ReverseMap();
            CreateMap<Positions, PositionListDto>().ReverseMap();


            CreateMap<UserAppDtoWithoutPhoto, UserApp>().ReverseMap();
            CreateMap<UserAppDtoWithoutPhoto, UserAppDto>().ReverseMap();


            CreateMap<UserAppDtoWithoutPhoto, support_UsersGenericList>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.externalCode, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<UserAppDto, support_UsersGenericList>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.externalCode, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            #region Budget

            CreateMap<BudgetPeriodListDto, BudgetPeriodInsertDto>().ReverseMap();
            CreateMap<BudgetPeriodListDto, BudgetPeriodUpdateDto>().ReverseMap();
            CreateMap<BudgetPeriod, BudgetPeriodListDto>().ReverseMap();



            CreateMap<BudgetPeriodUserListDto, BudgetPeriodUserInsertDto>().ReverseMap();
            CreateMap<BudgetPeriodUserListDto, BudgetPeriodUserUpdateDto>().ReverseMap();
            CreateMap<BudgetPeriodUser, BudgetPeriodUserListDto>().ReverseMap();
            CreateMap<BudgetPeriod, BudgetPeriodUserListDto>().ReverseMap();

            CreateMap<BudgetPeriodUser, BudgetPeriodUserListDto>().ReverseMap();

            CreateMap<BudgetJobCodeRequestListDto, BudgetJobCodeRequestInsertDto>().ReverseMap();
            CreateMap<BudgetJobCodeRequestListDto, BudgetJobCodeRequestUpdateDto>().ReverseMap();
            CreateMap<BudgetJobCodeRequest, BudgetJobCodeRequestListDto>().ReverseMap();



            CreateMap<BudgetNormCodeRequestListDto, BudgetNormCodeRequest>().ReverseMap();

            CreateMap<BudgetNormCodeRequestListDto, BudgetNormCodeRequestInsertDto>().ReverseMap();
            CreateMap<BudgetNormCodeRequestListDto, BudgetNormCodeRequestUpdateDto>().ReverseMap();
            CreateMap<BudgetNormCodeRequest, BudgetNormCodeRequestListDto>().ReverseMap();
            CreateMap<BudgetNormCodeRequest, BudgetNormCodeRequestListOnlyCodeDto>().ReverseMap();




            CreateMap<ApproveItemsDto, ApproveItems>().ReverseMap();

            CreateMap<WorkFlowHeadDto, WorkflowHead>().ReverseMap();


            CreateMap<WorkFlowHeadDtoWithoutItems, WorkflowHead>().ReverseMap();
            CreateMap<WorkFlowHeadDtoResultStartOrContinue, WorkflowHead>().ReverseMap();



            CreateMap<WorkFlowItemDto, WorkflowItem>().ReverseMap();

            CreateMap<WorkFlowItemDtoWithApproveItems, WorkflowItem>().ReverseMap();


            CreateMap<WorkFlowDefinationListDto, WorkFlowDefination>().ReverseMap();
            CreateMap<WorkFlowDefinationInsertDto, WorkFlowDefination>().ReverseMap();
            CreateMap<WorkFlowDefinationUpdateDto, WorkFlowDefination>().ReverseMap();




            CreateMap<PickListDto, PickListDTO>()
            .ForMember(dest => dest.label_localized, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.externalCode, opt => opt.MapFrom(src => src.externalCode)).ReverseMap();




            CreateMap<BudgetPromotionRequestListDto, BudgetPromotionRequestInsertDto>().ReverseMap();
            CreateMap<BudgetPromotionRequestListDto, BudgetPromotionRequestUpdateDto>().ReverseMap();
            CreateMap<BudgetPromotionRequest, BudgetPromotionRequestListDto>().ReverseMap();




            CreateMap<BudgetAdminUserListDto, BudgetAdminUserInsertDto>().ReverseMap();
            CreateMap<BudgetAdminUserListDto, BudgetAdminUserUpdateDto>().ReverseMap();
            CreateMap<BudgetAdminUser, BudgetAdminUserListDto>().ReverseMap();


            #endregion


            CreateMap<TicketDepartmensListDto, TicketDepartment>().ReverseMap();
            CreateMap<TicketDepartmensListDto, TicketDepartmensInsertDto>().ReverseMap();
            CreateMap<TicketDepartmensListDto, TicketDepartmensUpdateDto>().ReverseMap();

            CreateMap<DepartmentUserListDto, DepartmentUser>().ReverseMap();
            CreateMap<DepartmentUserListDto, DepartmentUserInsertDto>().ReverseMap();
            CreateMap<DepartmentUserListDto, DepartmentUserUpdateDto>().ReverseMap();

            CreateMap<Menu, MenuListDto>().ReverseMap();
            CreateMap<Menu, MenuUpdateDto>().ReverseMap();
            CreateMap<MenuListDto, MenuInsertDto>().ReverseMap();
            CreateMap<MenuListDto, MenuUpdateDto>().ReverseMap();
            CreateMap<RoleMenuListDto, RoleMenuInsertDto>().ReverseMap();
            CreateMap<RoleMenuListDto, AspNetRolesMenu>().ReverseMap();
            CreateMap<RoleTenantMenuListDto, AspNetRolesTenantMenu>().ReverseMap();
            CreateMap<FormDataListDto, Form>().ReverseMap();
            CreateMap<TicketTeam, TicketTeamListDto>().ReverseMap();
            CreateMap<TicketTeamListDto, TicketTeamInsertDto>().ReverseMap();
            CreateMap<TicketTeamListDto, TicketTeamUpdateDto>().ReverseMap();
            CreateMap<TicketTeamUserAppListDto, TicketTeamUserAppInsertDto>().ReverseMap();
            CreateMap<TicketTeamUserApp, TicketTeamUserAppInsertDto>().ReverseMap();
            CreateMap<WorkCompany, WorkCompanyDto>().ReverseMap();
            CreateMap<WorkCompanyInsertDto, WorkCompanyDto>().ReverseMap();
            CreateMap<WorkCompanyUpdateDto, WorkCompanyDto>().ReverseMap();
            CreateMap<WorkCompanySystemInfo, WorkCompanySystemInfoListDto>().ReverseMap();
            CreateMap<WorkCompanySystemInfoInsertDto, WorkCompanySystemInfoListDto>().ReverseMap();
            CreateMap<WorkCompanySystemInfoUpdateDto, WorkCompanySystemInfoListDto>().ReverseMap();
            CreateMap<WorkCompanyTicketMatris, WorkCompanyTicketMatrisListDto>().ReverseMap();
            CreateMap<WorkCompanyTicketMatrisInsertDto, WorkCompanyTicketMatrisListDto>().ReverseMap();
            CreateMap<WorkCompanyTicketMatrisUpdateDto, WorkCompanyTicketMatrisListDto>().ReverseMap();
            CreateMap<WorkCompanyTicketMatris, WorkCompanyTicketMatrisUpdateDto>().ReverseMap();
            CreateMap<Tickets, TicketInsertDto>().ReverseMap();
            CreateMap<Tickets, TicketListDto>().ReverseMap();
            CreateMap<TicketComment, TicketCommentDto>().ReverseMap();
            CreateMap<TicketComment, TicketCommentInsertDto>().ReverseMap();
            CreateMap<TicketFileDto, TicketFile>().ReverseMap();
            CreateMap<TicketFileInsertDto, TicketFile>().ReverseMap();
            CreateMap<Tickets, TicketUpdateManagerDto>().ReverseMap();
            CreateMap<Tickets, TicketUpdateDto>().ReverseMap();
            CreateMap<TicketFileInsertDto, TicketFile>().ReverseMap();
            CreateMap<TicketAssigneDto, TicketAssigne>().ReverseMap();
            CreateMap<DepartmentUser, DepartmentUserInsertDto>().ReverseMap();


            CreateMap<TicketInsertDto, Tickets>()
                .ForMember(dest => dest.TicketComment, opt => opt.MapFrom(src => src.TicketComment));

            CreateMap<TicketNotifications, TicketNotificationsInsertDto>().ReverseMap();
            CreateMap<TicketNotificationsListDto, TicketNotificationsInsertDto>().ReverseMap();
            CreateMap<TicketNotificationsListDto, TicketNotifications>().ReverseMap();



            CreateMap<TicketRuleEngineListDto, TicketRuleEngine>().ReverseMap();
            CreateMap<TicketRuleEngineInsertDto, TicketRuleEngineListDto>().ReverseMap();
            CreateMap<TicketRuleEngineUpdateDto, TicketRuleEngineListDto>().ReverseMap();



            CreateMap<Positions, PositionListDto>().ReverseMap();
            CreateMap<CreatePositionDto, UpdatePositionDto>().ReverseMap();
            CreateMap<CreatePositionDto, PositionListDto>().ReverseMap();
            CreateMap<UpdatePositionDto, PositionListDto>().ReverseMap();
            CreateMap<UpdatePositionDto, Positions>().ReverseMap();


            CreateMap<UserCalendarListDto, UserCalendar>().ReverseMap();
            CreateMap<UserCalendarInsertDto, UserCalendarListDto>().ReverseMap();
            CreateMap<UserCalendarUpdateDto, UserCalendarListDto>().ReverseMap();


            CreateMap<FormAssignDto, FormAssign>().ReverseMap();

            CreateMap<TicketProjectsListDto, TicketProjects>().ReverseMap();
            CreateMap<TicketProjectsInsertDto, TicketProjects>().ReverseMap();
            CreateMap<TicketProjectsUpdateDto, TicketProjects>().ReverseMap();

            CreateMap<PCTrackDto, PCTrack>().ReverseMap();

            CreateMap<FormAuthDto, FormAuth>().ReverseMap();
            CreateMap<FormAuthInsertDto, FormAuthDto>().ReverseMap();
            CreateMap<FormAuthUpdateDto, FormAuthDto>().ReverseMap();

            CreateMap<ProjectTasks, ProjectTasksListDto>().ReverseMap();
            CreateMap<ProjectTasks, ProjectTasksInsertDto>().ReverseMap();
            CreateMap<ProjectTasks, ProjectTasksUpdateDto>().ReverseMap();

            CreateMap<ProjectCategories, ProjectCategoriesInsertDto>().ReverseMap();
            CreateMap<ProjectCategories, ProjectCategoriesListDto>().ReverseMap();

            CreateMap<Inventory, InventoryListDto>().ReverseMap();
            CreateMap<Inventory, InventoryUpdateDto>().ReverseMap();
            CreateMap<Inventory, InventoryInsertDto>().ReverseMap();
            CreateMap<InventoryListDto, InventoryInsertDto>().ReverseMap();
            CreateMap<InventoryListDto, InventoryUpdateDto>().ReverseMap();

            CreateMap<Kanban, KanbanTasksListDto>().ReverseMap();

            // UserTenant
            CreateMap<UserTenant, UserTenantListDto>().ReverseMap();
            CreateMap<UserTenant, UserTenantInsertDto>().ReverseMap();
            CreateMap<UserTenant, UserTenantUpdateDto>().ReverseMap();

            // RoleTenant
            CreateMap<RoleTenant, vesa.core.DTOs.RoleTenants.RoleTenantListDto>().ReverseMap();
            CreateMap<RoleTenant, vesa.core.DTOs.RoleTenants.RoleTenantInsertDto>().ReverseMap();
            CreateMap<RoleTenant, vesa.core.DTOs.RoleTenants.RoleTenantUpdateDto>().ReverseMap();

            // UserTenantRole
            CreateMap<UserTenantRole, UserTenantRole>().ReverseMap();

			// CRM
			CreateMap<CustomerAddress, CustomerAddressDto>().ReverseMap();
			CreateMap<CustomerAddress, CustomerAddressInsertDto>().ReverseMap();
			CreateMap<CustomerAddress, CustomerAddressUpdateDto>().ReverseMap();

			CreateMap<CustomerOfficial, CustomerOfficialDto>().ReverseMap();

			CreateMap<CustomerEmail, CustomerEmailDto>().ReverseMap();
			CreateMap<CustomerEmail, CustomerEmailInsertDto>().ReverseMap();
			CreateMap<CustomerEmail, CustomerEmailUpdateDto>().ReverseMap();

			CreateMap<CustomerPhone, CustomerPhoneDto>().ReverseMap();
			CreateMap<CustomerPhone, CustomerPhoneInsertDto>().ReverseMap();
			CreateMap<CustomerPhone, CustomerPhoneUpdateDto>().ReverseMap();

			CreateMap<CustomerNote, CustomerNoteDto>().ReverseMap();
			CreateMap<CustomerNote, CustomerNoteInsertDto>().ReverseMap();
			CreateMap<CustomerNote, CustomerNoteUpdateDto>().ReverseMap();
			CreateMap<vesa.core.Models.CRM.Customer, CustomerListDto>()
				.ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.SecondaryEmails))
				.ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses))
				.ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones))
				.ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
				.ForMember(dest => dest.Officials, opt => opt.MapFrom(src => src.Officials))
				.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag).ToList()))
				.ForMember(dest => dest.Sectors, opt => opt.MapFrom(src => src.Sectors.Select(s => s.Sector).ToList()))
				.ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents.Select(d => d.FilePath).ToList()))
				.ForMember(dest => dest.CustomFields, opt => opt.MapFrom(src => src.CustomFields))
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.LegalName, opt => opt.MapFrom(src => src.LegalName))
				.ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
				.ForMember(dest => dest.TaxOffice, opt => opt.MapFrom(src => src.TaxOffice))
				.ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber))
				.ForMember(dest => dest.IsReferenceCustomer, opt => opt.MapFrom(src => src.IsReferenceCustomer))
				.ForMember(dest => dest.LogoFilePath, opt => opt.MapFrom(src => src.LogoFilePath))
				.ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
				.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
				.ForMember(dest => dest.TwitterUrl, opt => opt.MapFrom(src => src.TwitterUrl))
				.ForMember(dest => dest.FacebookUrl, opt => opt.MapFrom(src => src.FacebookUrl))
				.ForMember(dest => dest.LinkedinUrl, opt => opt.MapFrom(src => src.LinkedinUrl))
				.ForMember(dest => dest.InstagramUrl, opt => opt.MapFrom(src => src.InstagramUrl))
				.ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
				.ForMember(dest => dest.LifecycleStage, opt => opt.MapFrom(src => (int)src.LifecycleStage))
				.ForMember(dest => dest.NextActivityDate, opt => opt.MapFrom(src => src.NextActivityDate))
				.ForMember(dest => dest.CustomerTypeId, opt => opt.MapFrom(src => src.CustomerTypeId))
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Status) ? int.Parse(src.Status) : 0))
				.ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion));

			// Insert/Update: DTO -> Entity
			CreateMap<CustomerInsertDto, vesa.core.Models.CRM.Customer>()
				.ForMember(dest => dest.CustomerTypeId, opt => opt.MapFrom(src => src.CustomerTypeId));

			CreateMap<CustomerUpdateDto, vesa.core.Models.CRM.Customer>()
				.ForMember(dest => dest.CustomerTypeId, opt => opt.MapFrom(src => src.CustomerTypeId))
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

			CreateMap<CustomerEmail, string>().ConvertUsing(x => x.Email);
			CreateMap<string, CustomerEmail>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src));

			CreateMap<CustomerTag, string>().ConvertUsing(x => x.Tag);
			CreateMap<string, CustomerTag>().ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src));

			CreateMap<CustomerDocument, string>().ConvertUsing(x => x.FilePath);
			CreateMap<string, CustomerDocument>().ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src));

			CreateMap<CustomerDocument, CustomerDocumentDto>().ReverseMap();
			CreateMap<CustomerDocument, CustomerDocumentInsertDto>().ReverseMap();
			CreateMap<CustomerDocument, CustomerDocumentUpdateDto>().ReverseMap();

			CreateMap<CustomerSector, string>().ConvertUsing(x => x.Sector);
			CreateMap<string, CustomerSector>().ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src));

			CreateMap<CustomerCustomField, CustomFieldDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FieldId))
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.FieldType))
				.ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
				.ForMember(dest => dest.ValueJson, opt => opt.MapFrom(src => src.ValueJson))
				.ReverseMap()
				.ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FieldType, opt => opt.MapFrom(src => src.Type))
				.ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
				.ForMember(dest => dest.ValueJson, opt => opt.MapFrom(src => src.ValueJson));

			// Performans için optimize edilmiş mapping'ler
			CreateMap<vesa.core.Models.CRM.Customer, CustomerBasicDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.LegalName, opt => opt.MapFrom(src => src.LegalName))
				.ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
				.ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber))
				.ForMember(dest => dest.IsReferenceCustomer, opt => opt.MapFrom(src => src.IsReferenceCustomer))
				.ForMember(dest => dest.LogoFilePath, opt => opt.MapFrom(src => src.LogoFilePath))
				.ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
				.ForMember(dest => dest.TwitterUrl, opt => opt.MapFrom(src => src.TwitterUrl))
				.ForMember(dest => dest.FacebookUrl, opt => opt.MapFrom(src => src.FacebookUrl))
				.ForMember(dest => dest.LinkedinUrl, opt => opt.MapFrom(src => src.LinkedinUrl))
				.ForMember(dest => dest.InstagramUrl, opt => opt.MapFrom(src => src.InstagramUrl))
				.ForMember(dest => dest.LifecycleStage, opt => opt.MapFrom(src => (int)src.LifecycleStage))
				.ForMember(dest => dest.NextActivityDate, opt => opt.MapFrom(src => src.NextActivityDate))
				.ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
				.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
				.ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
				.ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion));
			
			CreateMap<CustomerBasicDto, vesa.core.Models.CRM.Customer>();

			// CRM Extras
			CreateMap<Opportunity, OpportunityDto>().ReverseMap();
			CreateMap<Activity, ActivityDto>().ReverseMap();
			CreateMap<Meeting, MeetingDto>().ReverseMap();
			CreateMap<Reminder, ReminderDto>().ReverseMap();
			CreateMap<Quote, QuoteDto>().ReverseMap();
			CreateMap<QuoteLine, QuoteLineDto>().ReverseMap();
			CreateMap<SpecialDay, SpecialDayDto>().ReverseMap();
			CreateMap<CrmChangeLog, CrmChangeLogDto>().ReverseMap();

			// Lookup
			CreateMap<LookupCategory, LookupCategoryDto>().ReverseMap();
			CreateMap<LookupItem, LookupItemDto>().ReverseMap();

            // Lookup
            CreateMap<LookupCategory, LookupCategoryDto>();
            CreateMap<LookupCategoryDto, LookupCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LookupItem, LookupItemDto>().ReverseMap();
            CreateMap<LookupModule, LookupModuleDto>().ReverseMap();
        }
    }
}
