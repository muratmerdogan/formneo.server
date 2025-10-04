using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.Inventory;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
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