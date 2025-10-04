using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.Ticket.TicketDepartments;
using formneo.core.DTOs.Ticket.Tickets;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.repository.Repositories;
using formneo.repository.UnitOfWorks;

namespace formneo.service.Services
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
