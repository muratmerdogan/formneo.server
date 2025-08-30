using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Models.Inventory;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
