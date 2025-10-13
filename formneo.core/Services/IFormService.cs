using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Models.FormEnums;

namespace formneo.core.Services
{
    public interface IFormService : IService<Form>
    {
        Task<Form> CreateRevisionAsync(Guid formId);
        Task<Form> PublishAsync(Guid formId);
        Task<IReadOnlyList<Form>> GetVersionsAsync(Guid parentFormId);
    }
}
