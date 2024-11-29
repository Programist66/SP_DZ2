using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_DZ2
{
    public class ProcessInfo : BindableBase
    {
        private int id;
        private string processName;
        private string memoryUsage;
        private int threadCount;
        private bool isResponding;

        public int Id
        {
            get => id;
        }

        public string ProcessName
        {
            get => processName;
            set 
            { 
                SetProperty(ref processName, value);
            }
        }

        public string MemoryUsage
        {
            get => memoryUsage;
            set 
            { 
                SetProperty(ref memoryUsage, value);
            }
        }

        public int ThreadCount
        {
            get => threadCount;
            set 
            { 
                SetProperty(ref threadCount, value);
            }
        }

        public bool IsResponding
        {
            get => isResponding;
            set 
            { 
                SetProperty(ref isResponding, value);
            }
        }

        public ProcessInfo(int id) 
        {
            this.id = id;
        }
    }
}
