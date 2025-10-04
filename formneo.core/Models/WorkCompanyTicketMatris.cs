using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace formneo.core.Models
{
    public class WorkCompanyTicketMatris : BaseEntity
    {
        [ForeignKey("WorkCompany")]
        public Guid? FromCompanyId { get; set; }

        public virtual WorkCompany? FromCompany { get; set; }

        [NotMapped]
        public List<Guid>? ToCompaniesIds { get; set; }

        public string? ToCompaniesIdsSerialized
        {
            get => ToCompaniesIds == null ? null : JsonSerializer.Serialize(ToCompaniesIds);
            set => ToCompaniesIds = string.IsNullOrEmpty(value) ? null : JsonSerializer.Deserialize<List<Guid>>(value);
        }
        public virtual List<WorkCompany>? ToCompanies { get; set; }

    }
}
