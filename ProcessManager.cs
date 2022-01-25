using System;
using System.Diagnostics;

namespace ProcessManager
{
    interface IProcessManager
    {
        public string KillById(int Id);
        public string KillByName(string Name);
        public string TrackList();
        public string ProcessId(string Name);
        public string ProcessName(int Id);
    }
    public class ProcessManager : IProcessManager
    {
        public ProcessManager() { }

        public string KillById(int Id)
        {
            try
            {
                foreach (var process in Process.GetProcesses())
                {
                    if (process.Id == Id)
                    {
                        process.Kill(true);
                        return "Process was successfully killed";
                    }
                }
            }
            catch (Exception)
            {
                return "There was some kind of error";
            }
            return "Process not found";
        }

        public string KillByName(string Name)
        {
            {
                try
                {
                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.ProcessName.ToLower() == Name)
                        {
                            process.Kill(true);
                            return "Process was successfully killed";
                        }
                    }
                }
                catch (Exception)
                {
                    return "There was some kind of error";
                }
                return "Process not found";
            }
        }

        public string ProcessId(string Name)
        {
            {
                try
                {
                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.ProcessName.ToLower() == Name)
                        {
                            return process.Id.ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    return "There was some kind of error";
                }
                return "Process not found";
            }
        }

        public string ProcessName(int Id)
        {
            {
                try
                {
                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.Id == Id)
                        {
                            return process.ProcessName;
                        }
                    }
                }
                catch (Exception)
                {
                    return "There was some kind of error";
                }
                return "Process not found";
            }
        }

        public string TrackList()
        {
            // var processes = Process.GetProcesses();
            var result = "";
            var proc_name = "";
            var wind_title = ""; 
            foreach (var process in Process.GetProcesses())
            {
                if (process.MachineName != "" && process.MainWindowTitle != "") 
                {
                    proc_name = process.MainWindowTitle;
                    wind_title = process.ProcessName;

                    if (proc_name.Length > 50)
                    {
                        proc_name = proc_name.Substring(0, 47) + "...";
                    }

                    if (wind_title.Length > 30)
                    {
                        wind_title = wind_title.Substring(0, 27) + "...";
                    }

                    //res = $"{process.Id,8}  {process.ProcessName,-30}  {name,-50}  {process.PagedMemorySize / 1024 / 1024} MB";
                    result += $"{process.Id, 10}  {process.ProcessName,-30}  {proc_name,-50}  {process.PagedMemorySize / 1024 / 1024, 10} MB\n";

                    // result += res.PadRight(width - res.Length);

                    // result += res;
                }
            }
            return result;
        }
    }
}