using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models
{
	public class ProjectRelation : BaseEntity
	{
		[ForeignKey("ParentProject")]
		public Guid ParentProjectId { get; set; }
		public virtual Project ParentProject { get; set; }

		[ForeignKey("ChildProject")]
		public Guid ChildProjectId { get; set; }
		public virtual Project ChildProject { get; set; }
	}
}


