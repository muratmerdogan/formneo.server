using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;
using vesa.core.Models.Inventory;
using vesa.core.Models.NewFolder;
using vesa.core.Models.PCTracking;
using vesa.core.Models.TaskManagement;
using vesa.core.Models.Ticket;
using vesa.core.Models.CRM;
using vesa.core.Models.Lookup;
using vesa.core.Services;


namespace vesa.repository
{
    public class AppDbContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public DbSet<MainClient> MainClients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<BudgetPeriod> BudgetPeriod { get; set; }
        public DbSet<WorkflowHead> WorkflowHead { get; set; }
        public DbSet<BudgetJobCodeRequest> BudgetJobCodeRequest { get; set; }
        public DbSet<BudgetNormCodeRequest> BudgetNormCodeRequest { get; set; }
        public DbSet<BudgetPromotionRequest> BudgetPromotionRequest { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<TicketComment> TicketComment { get; set; }
        public DbSet<TicketFile> TicketFile { get; set; }
        public DbSet<TicketNotifications> TicketNotifications { get; set; }

        public DbSet<TicketRuleEngine> TicketRuleEngine { get; set; }

        public DbSet<TicketDepartment> TicketDepartment { get; set; }
        public DbSet<AspNetRolesMenu> AspNetRolesMenu { get; set; }
        public DbSet<AspNetRolesTenantMenu> AspNetRolesTenantMenu { get; set; }
        public DbSet<TicketTeam> TicketTeam { get; set; }
        public DbSet<TicketTeamUserApp> TicketTeamUserApp { get; set; }

        //public DbSet<BudgetAdminUser> BudgetAdminUser { get; set; }
        public DbSet<FormRuleEngine> FormRuleEngine { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<FormRuntime> FormRuntime { get; set; }

        public DbSet<WorkCompany> WorkCompany { get; set; }
        public DbSet<UserTenant> UserTenants { get; set; }
        public DbSet<RoleTenant> RoleTenants { get; set; }
        public DbSet<UserTenantRole> UserTenantRoles { get; set; }

        public DbSet<WorkCompanySystemInfo> WorkCompanySystemInfo { get; set; }
        public DbSet<WorkCompanyTicketMatris> WorkCompanyTicketMatris { get; set; }

        public DbSet<DepartmentUser> DepartmentUsers { get; set; }
        public DbSet<FormAssign> FormAssign { get; set; }
        public DbSet<PCTrack> PCTrack { get; set; }
        public DbSet<Kanban> Kanban { get; set; }
        public DbSet<FormAuth> FormAuth { get; set; }
        public DbSet<TicketProjects> TicketProjects { get; set; }
        public DbSet<ProjectTasks> ProjectTasks { get; set; }
        public DbSet<ProjectCategories> ProjectCategories { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<UserCalendar> UserCalendar { get; set; }
        //public DbSet<MenuUI> MenuUI { get; set; }
        // CRM
        public DbSet<vesa.core.Models.CRM.Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerOfficial> CustomerOfficials { get; set; }
        public DbSet<CustomerEmail> CustomerEmails { get; set; }
        public DbSet<CustomerTag> CustomerTags { get; set; }
        public DbSet<CustomerDocument> CustomerDocuments { get; set; }
        public DbSet<CustomerSector> CustomerSectors { get; set; }
        public DbSet<CustomerCustomField> CustomerCustomFields { get; set; }
        public DbSet<CustomerPhone> CustomerPhones { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteLine> QuoteLines { get; set; }
        public DbSet<SpecialDay> SpecialDays { get; set; }
        public DbSet<CrmChangeLog> CrmChangeLogs { get; set; }

        // Lookup
        public DbSet<LookupCategory> LookupCategories { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }
        public DbSet<LookupModule> LookupModules { get; set; }

        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<EmpSalary> EmpSalary { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ITenantContext _tenantContext;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITenantContext tenantContext) : base(options)
        {

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _tenantContext = tenantContext;

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tickets>()
    .HasOne(t => t.TicketProject)
    .WithMany()
    .HasForeignKey(t => t.TicketProjectId)
    .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Project>()
            .Property(p => p.Category)
            .HasConversion<int>();

            modelBuilder.Entity<BudgetPeriod>(entity =>
            {
                entity.HasKey(bp => new { bp.PeriodCode });

            });


            modelBuilder.Entity<BudgetJobCodeRequest>(entity =>
            {
                entity.HasKey(bp => new { bp.Id });

            });


            modelBuilder.Entity<BudgetPeriodUser>(entity =>
            {
                entity.HasKey(bpu => bpu.Id);

                entity
                    .HasOne(bpu => bpu.BudgetPeriod)
                    .WithMany(bp => bp.BudgetPeriodUsers)
                    .HasForeignKey(bpu => new { bpu.BudgetPeriodCode });
            });


            modelBuilder.Entity<Tickets>()
                       .Property(e => e.UniqNumber)
                       .ValueGeneratedOnAdd()
                       .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);  // Güncelleme engellenir


            modelBuilder.Entity<BudgetPeriodUser>()
                   .HasIndex(bp => new { bp.BudgetPeriodCode, bp.UserName, bp.requestType })
                     .IsUnique();

            modelBuilder.Entity<BudgetAdminUser>()
            .HasOne(b => b.Company)
            .WithMany()
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.NoAction); // Cascade'i kap


            // modelBuilder.pro<int>().Where(p => p.Name == "OrderId").Configure(c => c.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity))
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<TicketDepartment>()
            .HasOne(td => td.Manager)
            .WithMany() // UserApp tarafında ters bir koleksiyon tanımlamıyorsanız WithMany boş bırakılır
            .HasForeignKey(td => td.ManagerId)
            .OnDelete(DeleteBehavior.Restrict); // İlişki silinme davranışı


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.NoAction; // veya DeleteBehavior.SetNull
                }
            }

            // CRM Enum configurations are handled in CustomerConfiguration.cs

            // PostgreSQL için RowVersion konfigürasyonu
            modelBuilder.Entity<vesa.core.Models.CRM.Customer>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerAddress>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerEmail>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerPhone>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerNote>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerTag>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerSector>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerDocument>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerCustomField>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerOfficial>()
                .Property(p => p.RowVersion)
                .IsRowVersion()
                .HasDefaultValue(new byte[8]);

            modelBuilder.Entity<CustomerAddress>()
                .Property(p => p.Type)
                .HasConversion<int>();

            modelBuilder.Entity<CustomerOfficial>()
                .Property(p => p.Role)
                .HasConversion<int>();

            modelBuilder.Entity<Opportunity>()
                .Property(p => p.Stage).HasConversion<int>();
            modelBuilder.Entity<Activity>()
                .Property(p => p.Type).HasConversion<int>();
            modelBuilder.Entity<Activity>()
                .Property(p => p.Status).HasConversion<int>();
            modelBuilder.Entity<Quote>()
                .Property(p => p.Status).HasConversion<int>();

            // Global tenant query filter for all BaseEntity (exclude GlobalBaseEntity)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (clrType == null) continue;
                if (typeof(GlobalBaseEntity).IsAssignableFrom(clrType)) continue;
                if (!typeof(BaseEntity).IsAssignableFrom(clrType)) continue;

                var parameter = System.Linq.Expressions.Expression.Parameter(clrType, "e");
                var isDeleteProp = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDelete));
                var notDeleted = System.Linq.Expressions.Expression.Not(isDeleteProp);

                var mainClientProp = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.MainClientId));
                var ctxConst = System.Linq.Expressions.Expression.Constant(this);
                var tenantProp = System.Linq.Expressions.Expression.Property(ctxConst, nameof(CurrentTenantIdForFilter));
                var equalsTenant = System.Linq.Expressions.Expression.Equal(mainClientProp, tenantProp);

                var body = System.Linq.Expressions.Expression.AndAlso(notDeleted, equalsTenant);

                var lambda = System.Linq.Expressions.Expression.Lambda(body, parameter);
                modelBuilder.Entity(clrType).HasQueryFilter(lambda);
            }
            // Lookup Soft Delete Query Filters
            modelBuilder.Entity<LookupModule>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<LookupCategory>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<LookupItem>().HasQueryFilter(e => !e.IsDelete);

            // Hybrid lookup unique constraints (global + tenant override)
            modelBuilder.Entity<LookupCategory>()
                .HasIndex(c => new { c.ModuleId, c.Key, c.TenantId })
                .IsUnique();

            modelBuilder.Entity<LookupItem>()
                .HasIndex(i => new { i.CategoryId, i.Code, i.TenantId })
                .IsUnique();

            // Per-tenant unique constraints (örnek: Customer.Code)
            modelBuilder.Entity<vesa.core.Models.CRM.Customer>()
                .HasIndex(c => new { c.MainClientId, c.Code })
                .IsUnique();
            modelBuilder.Entity<CustomerAddress>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerOfficial>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerEmail>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerTag>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerDocument>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerSector>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerCustomField>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerPhone>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CustomerNote>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<Opportunity>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<Activity>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<Reminder>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<Meeting>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<Quote>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<QuoteLine>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<SpecialDay>().HasQueryFilter(e => !e.IsDelete);
            modelBuilder.Entity<CrmChangeLog>().HasQueryFilter(e => !e.IsDelete);


            base.OnModelCreating(modelBuilder);






        }
        // Tenant-aware query filter backing field (reads from ITenantContext)
        private Guid? CurrentTenantIdForFilter => _tenantContext?.CurrentTenantId;
        private (Guid tenantId, Guid companyId, Guid plantId) ResolveContextIds()
        {
            Guid tenantId = Guid.Empty;
            Guid companyId = Guid.Empty;
            Guid plantId = Guid.Empty;

            try
            {
                var httpContext = _httpContextAccessor?.HttpContext;

                string headerTenant = httpContext?.Request?.Headers?["X-Tenant-Id"].FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(headerTenant) && Guid.TryParse(headerTenant, out var tid)) tenantId = tid;

                // HTTP isteği YOKSA (örn. seed, background job), appsettings fallback uygula.
                // HTTP isteği VARSA ve header boşsa, fallback kullanma: tenant damgası yapılmasın.
                if (tenantId == Guid.Empty && httpContext == null)
                {
                    var cfgTenant = _configuration?["MultiTenancy:DefaultMainClientId"];
                    if (!string.IsNullOrWhiteSpace(cfgTenant) && Guid.TryParse(cfgTenant, out var tid2)) tenantId = tid2;
                }

                // Company/Plant için de yalnızca HTTP context yoksa fallback uygula
                if (httpContext == null)
                {
                    var cfgCompany = _configuration?["MultiTenancy:DefaultCompanyId"];
                    if (!string.IsNullOrWhiteSpace(cfgCompany) && Guid.TryParse(cfgCompany, out var cid)) companyId = cid;

                    var cfgPlant = _configuration?["MultiTenancy:DefaultPlantId"];
                    if (!string.IsNullOrWhiteSpace(cfgPlant) && Guid.TryParse(cfgPlant, out var pid)) plantId = pid;
                }
            }
            catch { }

            return (tenantId, companyId, plantId);
        }

        public override int SaveChanges()
        {
            var ids = ResolveContextIds();


            try
            {
                //var currentUserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                foreach (var item in ChangeTracker.Entries())
                {
                    if (item.Entity is BaseEntity entityReference)
                    {
                        switch (item.Entity)
                        {
                            case EntityState.Added:
                                {
                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";

                                    //var customHeaderValue = _httpContextAccessor.HttpContext?.Request?.Headers["X-Custom-Header"].FirstOrDefault();


                                    Entry(entityReference).Property(x => x.UpdatedBy).IsModified = false;
                                    Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;




                                    entityReference.CreatedDate = DateTime.Now;
                                    if (_httpContextAccessor?.HttpContext != null && ids.tenantId == Guid.Empty)
                                    {
                                        throw new InvalidOperationException($"Tenant context is required for write operations for entity {item.Entity.GetType().Name}. Provide X-Tenant-Id header.");
                                    }
                                    if (ids.tenantId != Guid.Empty) entityReference.MainClientId = ids.tenantId;
                                    if (ids.companyId != Guid.Empty) entityReference.CompanyId = ids.companyId;
                                    if (ids.plantId != Guid.Empty) entityReference.PlantId = ids.plantId;
                                    if (entityReference.Id == Guid.Empty) entityReference.Id = Guid.NewGuid();




                                    if (userName != null)
                                        entityReference.CreatedBy = userName;
                                    else
                                        entityReference.CreatedBy = "nulldata";


                                    entityReference.UpdatedBy = "";

                                    // WorkflowHeadId alanını kontrol edip null olarak ayarlama
                                    if (entityReference is BudgetJobCodeRequest budgetJobCodeRequest)
                                    {
                                        budgetJobCodeRequest.WorkflowHeadId = budgetJobCodeRequest.WorkflowHeadId == Guid.Empty ? (Guid?)null : budgetJobCodeRequest.WorkflowHeadId;
                                    }

                                    if (entityReference is BudgetNormCodeRequest normrequest)
                                    {
                                        normrequest.WorkflowHeadId = normrequest.WorkflowHeadId == Guid.Empty ? (Guid?)null : normrequest.WorkflowHeadId;
                                    }


                                    break;
                                }
                            case EntityState.Modified:
                                {

                                    //var customHeaderValue = _httpContextAccessor.HttpContext?.Request?.Headers["X-Custom-Header"].FirstOrDefault();

                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";



                                    if (_httpContextAccessor?.HttpContext != null && ids.tenantId == Guid.Empty)
                                    {
                                        throw new InvalidOperationException($"Tenant context is required for write operations for entity {item.Entity.GetType().Name}. Provide X-Tenant-Id header.");
                                    }
                                    if (ids.tenantId != Guid.Empty) entityReference.MainClientId = ids.tenantId;
                                    if (ids.companyId != Guid.Empty) entityReference.CompanyId = ids.companyId;
                                    if (ids.plantId != Guid.Empty) entityReference.PlantId = ids.plantId;
                                    entityReference.UpdatedDate = DateTime.Now;

                                    if (userName != null)
                                        entityReference.UpdatedBy = userName;
                                    else
                                        entityReference.UpdatedBy = "nulldata";


                                    Entry(entityReference).Property(x => x.UniqNumber).IsModified = false;
                                    Entry(entityReference).Property(x => x.CreatedBy).IsModified = false;
                                    Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;


                                    // WorkflowHeadId alanını kontrol edip null olarak ayarlama
                                    if (entityReference is BudgetJobCodeRequest budgetJobCodeRequest)
                                    {
                                        budgetJobCodeRequest.WorkflowHeadId = budgetJobCodeRequest.WorkflowHeadId == Guid.Empty ? (Guid?)null : budgetJobCodeRequest.WorkflowHeadId;
                                    }

                                    if (entityReference is BudgetNormCodeRequest normrequest)
                                    {
                                        normrequest.WorkflowHeadId = normrequest.WorkflowHeadId == Guid.Empty ? (Guid?)null : normrequest.WorkflowHeadId;
                                    }

                                    break;
                                }
                        }
                        // CRM change logging for BaseEntity
                        try
                        {
                            var entityName = item.Entity.GetType().Name;
                            var entityId = entityReference.Id;
                            var changedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";
                            var changedDate = DateTime.Now;

                            if (item.State == EntityState.Added)
                            {
                                CrmChangeLog log = new CrmChangeLog
                                {
                                    EntityName = entityName,
                                    EntityId = entityId,
                                    Action = CrmChangeAction.Added,
                                    PropertyName = "*",
                                    OldValue = null,
                                    NewValue = null,
                                    ChangedBy = changedBy,
                                    ChangedDate = changedDate
                                };
                                // Try to set CustomerId if exists
                                var custProp = item.Entity.GetType().GetProperty("CustomerId");
                                if (custProp != null)
                                {
                                    var val = custProp.GetValue(item.Entity);
                                    if (val is Guid gid) log.CustomerId = gid;
                                }
                                CrmChangeLogs.Add(log);
                            }
                            else if (item.State == EntityState.Modified)
                            {
                                foreach (var prop in item.Properties)
                                {
                                    if (!prop.IsModified) continue;
                                    var propName = prop.Metadata.Name;
                                    if (propName == nameof(BaseEntity.UpdatedDate) || propName == nameof(BaseEntity.UpdatedBy) || propName == nameof(BaseEntity.UniqNumber) || propName == nameof(BaseEntity.CreatedBy) || propName == nameof(BaseEntity.CreatedDate)) continue;
                                    if (propName == nameof(BaseEntity.MainClientId) || propName == nameof(BaseEntity.CompanyId) || propName == nameof(BaseEntity.PlantId)) continue;

                                    string oldVal = prop.OriginalValue?.ToString();
                                    string newVal = prop.CurrentValue?.ToString();
                                    if (oldVal == newVal) continue;

                                    CrmChangeLog log = new CrmChangeLog
                                    {
                                        EntityName = entityName,
                                        EntityId = entityId,
                                        Action = CrmChangeAction.Modified,
                                        PropertyName = propName,
                                        OldValue = oldVal,
                                        NewValue = newVal,
                                        ChangedBy = changedBy,
                                        ChangedDate = changedDate
                                    };
                                    var custProp = item.Entity.GetType().GetProperty("CustomerId");
                                    if (custProp != null)
                                    {
                                        var val = custProp.GetValue(item.Entity);
                                        if (val is Guid gid) log.CustomerId = gid;
                                    }
                                    CrmChangeLogs.Add(log);
                                }
                            }
                        }
                        catch {}
                    }

                    // Global varlıklar için: tenant alanları yok, sadece temel iz bilgilerinin yönetimi
                    if (item.Entity is GlobalBaseEntity globalEntity)
                    {
                        switch (item.State)
                        {
                            case EntityState.Added:
                                {
                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";
                                    Entry(globalEntity).Property(x => x.UpdatedBy).IsModified = false;
                                    Entry(globalEntity).Property(x => x.UpdatedDate).IsModified = false;

                                    globalEntity.Id = globalEntity.Id == Guid.Empty ? Guid.NewGuid() : globalEntity.Id;
                                    globalEntity.CreatedDate = DateTime.Now;
                                    globalEntity.CreatedBy = userName ?? "nulldata";
                                    globalEntity.UpdatedBy = "";
                                    break;
                                }
                            case EntityState.Modified:
                                {
                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";
                                    Entry(globalEntity).Property(x => x.UniqNumber).IsModified = false;
                                    Entry(globalEntity).Property(x => x.CreatedBy).IsModified = false;
                                    Entry(globalEntity).Property(x => x.CreatedDate).IsModified = false;

                                    globalEntity.UpdatedDate = DateTime.Now;
                                    globalEntity.UpdatedBy = userName ?? "nulldata";
                                    break;
                                }
                        }
                        // CRM change logging for GlobalBaseEntity
                        try
                        {
                            var entityName = item.Entity.GetType().Name;
                            var entityId = globalEntity.Id;
                            var changedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";
                            var changedDate = DateTime.Now;

                            if (item.State == EntityState.Added)
                            {
                                CrmChangeLog log = new CrmChangeLog
                                {
                                    EntityName = entityName,
                                    EntityId = entityId,
                                    Action = CrmChangeAction.Added,
                                    PropertyName = "*",
                                    OldValue = null,
                                    NewValue = null,
                                    ChangedBy = changedBy,
                                    ChangedDate = changedDate
                                };
                                var custProp = item.Entity.GetType().GetProperty("CustomerId");
                                if (custProp != null)
                                {
                                    var val = custProp.GetValue(item.Entity);
                                    if (val is Guid gid) log.CustomerId = gid;
                                }
                                CrmChangeLogs.Add(log);
                            }
                            else if (item.State == EntityState.Modified)
                            {
                                foreach (var prop in item.Properties)
                                {
                                    if (!prop.IsModified) continue;
                                    var propName = prop.Metadata.Name;
                                    if (propName == nameof(GlobalBaseEntity.UpdatedDate) || propName == nameof(GlobalBaseEntity.UpdatedBy) || propName == nameof(GlobalBaseEntity.UniqNumber) || propName == nameof(GlobalBaseEntity.CreatedBy) || propName == nameof(GlobalBaseEntity.CreatedDate)) continue;

                                    string oldVal = prop.OriginalValue?.ToString();
                                    string newVal = prop.CurrentValue?.ToString();
                                    if (oldVal == newVal) continue;

                                    CrmChangeLog log = new CrmChangeLog
                                    {
                                        EntityName = entityName,
                                        EntityId = entityId,
                                        Action = CrmChangeAction.Modified,
                                        PropertyName = propName,
                                        OldValue = oldVal,
                                        NewValue = newVal,
                                        ChangedBy = changedBy,
                                        ChangedDate = changedDate
                                    };
                                    var custProp = item.Entity.GetType().GetProperty("CustomerId");
                                    if (custProp != null)
                                    {
                                        var val = custProp.GetValue(item.Entity);
                                        if (val is Guid gid) log.CustomerId = gid;
                                    }
                                    CrmChangeLogs.Add(log);
                                }
                            }
                        }
                        catch {}
                    }
                }


            }
            catch (Exception ex)
            {


            }
            return base.SaveChanges();

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var ids = ResolveContextIds();

            try
            {
                foreach (var item in ChangeTracker.Entries())
                {
                    if (item.Entity is BaseEntity entityReference)
                    {
                        switch (item.State)
                        {
                            case EntityState.Added:
                                {

                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";


                                    //var customHeaderValue = _httpContextAccessor.HttpContext?.Request?.Headers["X-Custom-Header"].FirstOrDefault();


                                    Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                                    Entry(entityReference).Property(x => x.UpdatedBy).IsModified = false;

                                    entityReference.CreatedDate = DateTime.Now;
                                    if (ids.tenantId != Guid.Empty) entityReference.MainClientId = ids.tenantId;
                                    if (ids.companyId != Guid.Empty) entityReference.CompanyId = ids.companyId;
                                    if (ids.plantId != Guid.Empty) entityReference.PlantId = ids.plantId;
                                    entityReference.Id = Guid.NewGuid();

                                    if (userName != null)
                                        entityReference.CreatedBy = userName;
                                    else
                                        entityReference.CreatedBy = "nulldata";

                                    entityReference.UpdatedBy = "";

                                    // WorkflowHeadId alanını kontrol edip null olarak ayarlama
                                    if (entityReference is BudgetJobCodeRequest budgetJobCodeRequest)
                                    {
                                        budgetJobCodeRequest.WorkflowHeadId = budgetJobCodeRequest.WorkflowHeadId == Guid.Empty ? (Guid?)null : budgetJobCodeRequest.WorkflowHeadId;
                                    }

                                    if (entityReference is BudgetNormCodeRequest normrequest)
                                    {
                                        normrequest.WorkflowHeadId = normrequest.WorkflowHeadId == Guid.Empty ? (Guid?)null : normrequest.WorkflowHeadId;
                                    }

                                    break;
                                }
                            case EntityState.Modified:
                                {

                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";

                                    //var customHeaderValue = _httpContextAccessor.HttpContext?.Request?.Headers["X-Custom-Header"].FirstOrDefault();


                                    Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                    Entry(entityReference).Property(x => x.CreatedBy).IsModified = false;
                                    Entry(entityReference).Property(x => x.UniqNumber).IsModified = false;

                                    if (ids.tenantId != Guid.Empty) entityReference.MainClientId = ids.tenantId;
                                    if (ids.companyId != Guid.Empty) entityReference.CompanyId = ids.companyId;
                                    if (ids.plantId != Guid.Empty) entityReference.PlantId = ids.plantId;
                                    entityReference.UpdatedDate = DateTime.Now;

                                    if (userName != null)
                                        entityReference.UpdatedBy = userName;
                                    else
                                        entityReference.UpdatedBy = "nulldata";

                                    if (entityReference is BudgetJobCodeRequest budgetJobCodeRequest)
                                    {
                                        budgetJobCodeRequest.WorkflowHeadId = budgetJobCodeRequest.WorkflowHeadId == Guid.Empty ? (Guid?)null : budgetJobCodeRequest.WorkflowHeadId;
                                    }

                                    break;
                                }


                        }
                    }

                    // Global varlıklar için Created/Updated alanları yönetimi
                    if (item.Entity is GlobalBaseEntity globalEntity)
                    {
                        switch (item.State)
                        {
                            case EntityState.Added:
                                {
                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";

                                    Entry(globalEntity).Property(x => x.UpdatedDate).IsModified = false;
                                    Entry(globalEntity).Property(x => x.UpdatedBy).IsModified = false;

                                    globalEntity.CreatedDate = DateTime.Now;
                                    globalEntity.Id = globalEntity.Id == Guid.Empty ? Guid.NewGuid() : globalEntity.Id;

                                    if (userName != null)
                                        globalEntity.CreatedBy = userName;
                                    else
                                        globalEntity.CreatedBy = "nulldata";

                                    globalEntity.UpdatedBy = "";

                                    break;
                                }
                            case EntityState.Modified:
                                {
                                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "defaultUser";

                                    Entry(globalEntity).Property(x => x.CreatedDate).IsModified = false;
                                    Entry(globalEntity).Property(x => x.CreatedBy).IsModified = false;
                                    Entry(globalEntity).Property(x => x.UniqNumber).IsModified = false;

                                    globalEntity.UpdatedDate = DateTime.Now;
                                    if (userName != null)
                                        globalEntity.UpdatedBy = userName;
                                    else
                                        globalEntity.UpdatedBy = "nulldata";

                                    break;
                                }
                        }
                    }
                }
                var result = base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {


                return null;

            }

        }
    }
}
