using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace formneo.core.Models.Ticket
{
    [Index(nameof(TicketTeamId), nameof(UserAppId), IsUnique = true)]
    public class TicketTeamUserApp : BaseEntity
    {
        [ForeignKey("TicketTeam")]
        public Guid TicketTeamId { get; set; }
        public virtual TicketTeam TicketTeam { get; set; }

        [ForeignKey("UserApp")]
        public string UserAppId { get; set; }
        public virtual UserApp UserApp { get; set; }
    }
}