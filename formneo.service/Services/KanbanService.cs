using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.NewFolder;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.service.Services
{
    public class KanbanService : Service<Kanban>, IKanbanService
    {
        private readonly IKanbanRepository _kanbanRepository;

        public KanbanService(IGenericRepository<Kanban> repository, IUnitOfWork unitOfWork, IKanbanRepository kanbanRepository) : base(repository, unitOfWork)
        {
            _kanbanRepository = kanbanRepository;
        }
    }
}
