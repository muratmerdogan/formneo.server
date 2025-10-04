using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class PositionRepository : GenericRepository<Positions>, IPositionRepository
    {
        public PositionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
