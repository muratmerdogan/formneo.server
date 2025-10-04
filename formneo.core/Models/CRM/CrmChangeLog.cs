using System;

namespace formneo.core.Models.CRM
{
	public enum CrmChangeAction
	{
		Added,
		Modified,
		Deleted
	}

	public class CrmChangeLog : GlobalBaseEntity
	{
		public string EntityName { get; set; }
		public Guid EntityId { get; set; }
		public Guid? CustomerId { get; set; }
		public CrmChangeAction Action { get; set; }
		public string PropertyName { get; set; }
		public string OldValue { get; set; }
		public string NewValue { get; set; }
		public string ChangedBy { get; set; }
		public DateTime ChangedDate { get; set; }
	}
}


