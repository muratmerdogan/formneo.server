using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class ApproveItemsRepository : GenericRepository<ApproveItems>, IApproveItemsRepository
    {
        public ApproveItemsRepository(AppDbContext context) : base(context)
        {

        }

    }
}
