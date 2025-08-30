using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class PositionRepository : GenericRepository<Positions>, IPositionRepository
    {
        public PositionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
