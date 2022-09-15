﻿using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Xml.Linq;

public class PerformanceManager
{
    public static string GetCurrentCpuUsage()
    {
        PerformanceCounter cpuCounter;
        cpuCounter = new PerformanceCounter(categoryName: "Processor", counterName: "% Processor Time", instanceName: "_Total");
        return cpuCounter.NextValue() + "%";
    }

    public static string GetAvailableRAM()
    {
        PerformanceCounter ramCounter;
        ramCounter = new PerformanceCounter(categoryName: "Memory", counterName: "Available MBytes");
        return ramCounter.NextValue() + "MB";
    }


    public static string GetPerformanceCounter(string category, string counter, string instance = "")
    {
        PerformanceCounter CPUCounter;
        CPUCounter = new PerformanceCounter(categoryName: category, counterName: counter, instanceName: instance);
        CPUCounter.NextValue();
        Thread.Sleep(1000);
        return string.Format("{0:0.00}", CPUCounter.NextValue());
    }

    public static async Task SetInterval(Action action, TimeSpan timeout)
    {
        await Task.Delay(timeout).ConfigureAwait(false);

        action();

        SetInterval(action, timeout);
    }

    static async Task Main()
    {
        var getCurrentCpuUsage = string.Empty;
        var getAvailableRAM = string.Empty;
        while (true)
        {
            getCurrentCpuUsage = GetCurrentCpuUsage();
            getAvailableRAM = GetAvailableRAM();
            Console.WriteLine($"getCurrentCpuUsage: {getCurrentCpuUsage}, getAvailableRAM: {getAvailableRAM}");
            //SetInterval(() => Console.WriteLine($"getCurrentCpuUsage: {getCurrentCpuUsage}, getAvailableRAM: {getAvailableRAM}"), TimeSpan.FromSeconds(5));
            await Task.Delay(5000);
        }
       
        //Thread.Sleep(TimeSpan.FromMinutes(10));
    }
}