using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;
using formneo.core.Models;
using formneo.core.Services;

namespace formneo.api.Filters
{
	public sealed class GlobalEntityWriteInterceptor : SaveChangesInterceptor
	{
		public static volatile bool SkipEnforcement = false;
		private readonly ITenantContext _tenantContext;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public GlobalEntityWriteInterceptor(
			ITenantContext tenantContext,
			IHttpContextAccessor httpContextAccessor)
		{
			_tenantContext = tenantContext;
			_httpContextAccessor = httpContextAccessor;
		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			EnforceGlobalEntityRules(eventData.Context);
			return base.SavingChanges(eventData, result);
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			EnforceGlobalEntityRules(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private void EnforceGlobalEntityRules(DbContext? context)
		{
			if (context == null) return;
			if (SkipEnforcement) return;
			var httpContext = _httpContextAccessor.HttpContext;
			// Seeding/background contexts have no HttpContext: skip enforcement
			if (httpContext == null) return;
			bool isGlobalAdmin = false;
			if (httpContext.Items != null && httpContext.Items.ContainsKey("IsGlobalAdmin"))
			{
				isGlobalAdmin = httpContext.Items["IsGlobalAdmin"] as bool? ?? false;
			}

			var currentTenantId = _tenantContext?.CurrentTenantId;

			foreach (var entry in context.ChangeTracker.Entries())
			{
				if (entry.State != EntityState.Added && entry.State != EntityState.Modified && entry.State != EntityState.Deleted)
					continue;

				var entity = entry.Entity;
				if (entity == null) continue;

				var entityType = entity.GetType();
				bool isGlobalEntity = IsGlobalEntityByType(entityType);
				bool isTenantEntity = IsTenantEntityByType(entityType);

				// Special handling: Some GlobalBaseEntity types carry a nullable TenantId field to allow tenant overlays
				// Permit tenant writes only to their own overlay records; forbid tenant writes on true-global (TenantId == null)
				if (isGlobalEntity)
				{
					var tenantIdProperty = entityType.GetProperty("TenantId");
					if (tenantIdProperty != null)
					{
						var entityTenantId = (Guid?)tenantIdProperty.GetValue(entity);
						if (!isGlobalAdmin)
						{
							// Added
							if (entry.State == EntityState.Added)
							{
								if (currentTenantId == null || currentTenantId == Guid.Empty)
								{
									throw new InvalidOperationException("X-Tenant-Id header required for tenant entity write operations");
								}
								if (entityTenantId == null || entityTenantId == Guid.Empty)
								{
									// Prevent tenants from creating global records
									throw new InvalidOperationException("Write to global entity is not allowed for tenant users");
								}
								if (entityTenantId != currentTenantId)
								{
									throw new InvalidOperationException("Write to a different tenant's entity is not allowed");
								}
								continue;
							}

							// Modified or Deleted
							if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
							{
								if (entityTenantId == null || entityTenantId == Guid.Empty)
								{
									// Global record: tenants cannot modify/delete
									throw new InvalidOperationException("Write to global entity is not allowed for tenant users");
								}
								if (currentTenantId == null || currentTenantId == Guid.Empty)
								{
									throw new InvalidOperationException("X-Tenant-Id header required for tenant entity write operations");
								}
								if (entityTenantId != currentTenantId)
								{
									throw new InvalidOperationException("Write to a different tenant's entity is not allowed");
								}
								continue;
							}
						}
					}
				}

				// Enforce tenant header for tenant entities (Global Admin dahil)
				if (isTenantEntity)
				{
					if (currentTenantId == null || currentTenantId == Guid.Empty)
					{
						throw new InvalidOperationException("X-Tenant-Id header required for tenant entity write operations");
					}
					continue;
				}

				// Global entities: only Global Admin can write
				if (isGlobalEntity && !isGlobalAdmin)
				{
					throw new InvalidOperationException("Write to global entity is not allowed for tenant users");
				}
			}
		}

		private static bool IsGlobalEntityByType(Type entityType)
		{
			var t = entityType;
			while (t != null)
			{
				if (string.Equals(t.Name, "GlobalBaseEntity", StringComparison.Ordinal))
					return true;
				t = t.BaseType;
			}
			if (entityType.GetInterfaces().Any(i => string.Equals(i.Name, "IGlobalEntity", StringComparison.Ordinal)))
				return true;
			return false;
		}

		private static bool IsTenantEntityByType(Type entityType)
		{
			var t = entityType;
			while (t != null)
			{
				if (string.Equals(t.Name, "TenantBaseEntity", StringComparison.Ordinal))
					return true;
				t = t.BaseType;
			}
			if (entityType.GetInterfaces().Any(i => string.Equals(i.Name, "ITenantEntity", StringComparison.Ordinal)))
				return true;
			return false;
		}
	}
}
