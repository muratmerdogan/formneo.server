using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class FormInstanceRepository : GenericRepository<FormInstance>, IFormInstanceRepository
    {
        public FormInstanceRepository(AppDbContext context) : base(context)
        {
        }
    }
}

