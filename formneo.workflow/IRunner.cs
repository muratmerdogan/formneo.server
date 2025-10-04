using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.workflow
{
    interface  IRunner
    {
       void RunAsync(string workflowId);
    }
}
