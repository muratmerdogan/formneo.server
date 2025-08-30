using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.workflow
{
    interface  IRunner
    {
       void RunAsync(string workflowId);
    }
}
