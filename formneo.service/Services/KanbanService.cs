using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.NewFolder;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
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
