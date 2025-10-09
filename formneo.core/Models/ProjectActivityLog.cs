using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models
{
	public enum ProjectActivityType
	{
		TaskCreated = 1,
		TaskUpdated = 2,
		StatusChanged = 3,
		CommentAdded = 4
	}

	public class ProjectActivityLog : BaseEntity
	{
		[ForeignKey("ProjectTask")]
		public Guid ProjectTaskId { get; set; }
		public virtual ProjectTask ProjectTask { get; set; }

		public ProjectActivityType ActivityType { get; set; }

		// Özet metin (örn: "Status: Bekliyor -> Islemde")
		[StringLength(512)]
		public string? Summary { get; set; }

		// Eski/Yeni durum gibi detaylar
		[StringLength(2048)]
		public string? Details { get; set; }

		[ForeignKey("UserApp")]
		public string? UserId { get; set; }
		public virtual UserApp? User { get; set; }
	}
}


