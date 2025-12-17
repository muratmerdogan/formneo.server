using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
    public class FormInstanceService : Service<FormInstance>, IFormInstanceService
    {
        public FormInstanceService(IGenericRepository<FormInstance> repository, IUnitOfWork unitOfWork) 
            : base(repository, unitOfWork)
        {
        }
    }
}

