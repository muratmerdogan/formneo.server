using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class ApproveItemsRepository : GenericRepository<ApproveItems>, IApproveItemsRepository
    {
        public ApproveItemsRepository(AppDbContext context) : base(context)
        {

        }

    }
}
