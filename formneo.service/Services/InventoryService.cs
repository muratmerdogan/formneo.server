using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Inventory;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.service.Services
{
    public class InventoryService : Service<Inventory>, IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IGenericRepository<Inventory> repository, IUnitOfWork unitOfWork, IInventoryRepository inventoryRepository) : base(repository, unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
        }
    }
}