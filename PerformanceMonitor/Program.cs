using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Xml.Linq;

public class PerformanceManager
{
    static PerformanceCounter cpuCounter;
    static PerformanceCounter ramCounter;
    private static System.Timers.Timer syncTimer;

    PerformanceManager()
    {
        ramCounter = new PerformanceCounter(categoryName:"Memory", counterName:"Available MBytes");
        cpuCounter = new PerformanceCounter(categoryName: "Processor", counterName: "% Processor Time", instanceName: "_Total");
    }

    public string GetCurrentCpuUsage()
    {
        return cpuCounter.NextValue() + "%";
    }

    public string GetAvailableRAM()
    {
        return ramCounter.NextValue() + "MB";
    }


    public string GetPerformanceCounter(string category, string counter, string instance = "")
    {
        PerformanceCounter CPUCounter;
        CPUCounter = new PerformanceCounter(categoryName: category, counterName: counter, instanceName: instance);
        CPUCounter.NextValue();
        System.Threading.Thread.Sleep(1000);
        return String.Format("{0:0.00}", CPUCounter.NextValue());
    }

    private void HandleCheckShedule()
    {
        //Create a timer with a 120s interval
        var syncTimer = new System.Timers.Timer(120000);

        // Hook up the Elapsed event for the timer. 
        syncTimer.Elapsed += SynchronizeSchedule;
        syncTimer.AutoReset = true;
        syncTimer.Enabled = true;
    }

    private void SynchronizeSchedule(Object source, ElapsedEventArgs e)
    {
        
        
    }



    static void Main()
    {
        HandleGetPerformanceMonitor();
    }
}