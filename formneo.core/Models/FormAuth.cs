using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace formneo.core.Models
{
    public class FormAuth : BaseEntity
    {
        [ForeignKey("Form")]
        public Guid? FormId { get; set; }

        public virtual Form? Form { get; set; }

        [NotMapped]
        public List<Guid>? UserIds { get; set; }

        public string? UserIdsSerialized
        {
            get => UserIds == null ? null : JsonSerializer.Serialize(UserIds);
            set => UserIds = string.IsNullOrEmpty(value) ? null : JsonSerializer.Deserialize<List<Guid>>(value);
        }
        public virtual List<UserApp>? Users { get; set; }
    }
}
