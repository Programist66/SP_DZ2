using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Prism.Commands;

namespace SP_DZ2
{
    public class MainProcessVM
    {
        public ObservableCollection<ProcessVM> ProcessVMs { get; set; } = new();
        private DispatcherTimer timer;

        public ProcessVM SelectedProcess { get; set; }

        public MainProcessVM() 
        {
            RefreshProcesses();
            timer = new DispatcherTimer();
            timer.Tick += Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            CloseProcessCommand = new DelegateCommand(CloseProcess);
            RefreshProcessesCommand = new DelegateCommand(RefreshProcesses);
        }

        public ICommand CloseProcessCommand { get; set; }
        private void CloseProcess() 
        {
            if (SelectedProcess is null)
            {
                MessageBox.Show("Test");
                return;
            }
            Process currenntProcess = Process.GetProcessById(SelectedProcess.Process.Id);
            currenntProcess.CloseMainWindow();
            ProcessVMs.Remove(SelectedProcess);
        }

        public ICommand RefreshProcessesCommand { get; set; }
        private void RefreshProcesses()
        {
            Process[] AllProcesses = Process.GetProcesses();

            foreach (var process in AllProcesses)
            {
                var existingProcess = ProcessVMs.FirstOrDefault(proc => proc.Process.Id == process.Id);
                if (existingProcess != null)
                {
                    existingProcess.Process.ProcessName = process.ProcessName;
                    existingProcess.Process.MemoryUsage = (process.PrivateMemorySize64 / 1024).ToString(); // Convert to MB
                    existingProcess.Process.ThreadCount = process.Threads.Count;
                    existingProcess.Process.IsResponding = process.Responding;
                }
                else
                {
                    ProcessVMs.Add(new ProcessVM(new ProcessInfo(process.Id)
                    {
                        ProcessName = process.ProcessName,
                        MemoryUsage = (process.PrivateMemorySize64 / 1024).ToString(), // Convert to MB
                        ThreadCount = process.Threads.Count,
                        IsResponding = process.Responding
                    }));
                }
            }
            for (int i = ProcessVMs.Count - 1; i >= 0; i--)
            {
                if (!AllProcesses.Any(proc => proc.Id == ProcessVMs[i].Process.Id))
                {
                    ProcessVMs.RemoveAt(i);
                }
            }
        }

        private void Tick(object? sender, EventArgs e)
        {
            RefreshProcesses();
        }
    }
}
