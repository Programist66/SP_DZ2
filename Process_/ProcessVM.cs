using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_DZ2
{
    public class ProcessVM
    {
        public ProcessInfo Process { get; set; }

        public ProcessVM(ProcessInfo process)
        {
            Process = process;
        }
    }
}
