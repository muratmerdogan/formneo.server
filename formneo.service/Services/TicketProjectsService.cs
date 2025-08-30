using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.Ticket.Tickets;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository.Repositories;
using vesa.repository.UnitOfWorks;

namespace vesa.service.Services
{
    public class TicketProjectsService : Service<TicketProjects>, ITicketProjectsService
    {
        private readonly ITicketProjectsRepository _ticketProjectsRepository;

        public TicketProjectsService(IGenericRepository<TicketProjects> repository, IUnitOfWork unitOfWork, ITicketProjectsRepository ticketProjectsRepository) : base(repository, unitOfWork)
        {
            _ticketProjectsRepository = ticketProjectsRepository;
        }
    }
}
