using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class PlantRepository : GenericRepository<Plant>, IPlantRepository
    {
        public PlantRepository(AppDbContext context) : base(context)
        {
        }
    }
}
