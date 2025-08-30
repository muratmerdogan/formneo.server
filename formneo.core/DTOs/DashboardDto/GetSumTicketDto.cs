using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.DashboardDto
{
    public class GetSumTicketDto
    {
        public int DraftCount { get; set; }
        public int OpenCount { get; set; }
        public int AssignedCount { get; set; }
        public int ConsultantWaitingCount { get; set; }
        public int InProgressCount { get; set; }
        public int InternalTestingCount { get; set; }
        public int CustomerTestingCount { get; set; }
        public int WaitingForCustomerCount { get; set; }
        public int ResolvedCount { get; set; }
        public int CanceledCount { get; set; }
        public int ClosedCount { get; set; }
        public int InApproveCount { get; set; }
        public int ZeroCount { get; set; }
        public int SumCount { get; set; }
     
    }
}
