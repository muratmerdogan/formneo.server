using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models
{
	public class ProjectTaskComment : BaseEntity
	{
		[ForeignKey("ProjectTask")]
		public Guid ProjectTaskId { get; set; }
		public virtual ProjectTask ProjectTask { get; set; }

		[ForeignKey("UserApp")]
		public string UserId { get; set; }
		public virtual UserApp User { get; set; }

		[Required]
		[StringLength(4000)]
		public string Content { get; set; }
	}
}


