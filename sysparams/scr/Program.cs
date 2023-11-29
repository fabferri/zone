using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Collections;

namespace SysParams
{
    class Program
    {
        static void Main(string[] args)
        {
            String nl = Environment.NewLine;

            PollingParams.InitNetParams();
            ulong memTot = PollingParams.GetPhysicalMemory() / (1024 * 1024);

            Console.WriteLine("Total Phys Memory (MB): {0}", memTot);
            string sMachineName = Environment.MachineName.ToUpper();
            string sOSVersion = Environment.OSVersion.VersionString;
            string sGetProcessorCount = Environment.ProcessorCount.ToString();
            Console.WriteLine("========================================================");
            while (true)
            {

                float fCPUProcessorTime = PollingParams.cpuProcessorTime.NextValue();
                float fCPUPrivilegedTime = PollingParams.cpuPrivilegedTime.NextValue();
                float fCPUInterruptTime = PollingParams.cpuInterruptTime.NextValue();
                float fCPUDPCTime = PollingParams.cpuDPCTime.NextValue(); // <----
                float fPageFile = PollingParams.pageFile.NextValue();
                float fProcessorQueueLengh = PollingParams.processorQueueLengh.NextValue();
                float fMemAvailable = PollingParams.memAvailable.NextValue();
                float fgetMemCommitted = PollingParams.memCommited.NextValue() / (1024 * 1024);
                float fgetMemCachedBytes = PollingParams.memCachedBytes.NextValue() / (1024 * 1024);  // return the value of Memory Cached in MByte
                float fDiskQueueLengh = PollingParams.diskQueueLengh.NextValue();
                float fNetSent = PollingParams.getTrafficSent();
                float fNetReceived = PollingParams.getTrafficReceived();

                Console.WriteLine("Hostname            : {0}", sMachineName);
                //                Console.WriteLine("OS version      : {0}", sOSVersion);
                Console.WriteLine("Number Processor    : {0}", sGetProcessorCount);
                Console.WriteLine("WorkingSet          : {0}", Environment.WorkingSet);
                Console.WriteLine("CPU Processor  Time : {0} %", fCPUProcessorTime.ToString());
                Console.WriteLine("CPU Privileged Time : {0} %", fCPUPrivilegedTime.ToString());
                Console.WriteLine("CPU Interrupt  Time : {0} %", fCPUInterruptTime.ToString());
                Console.WriteLine("CPU DPC        Time : {0} %", fCPUDPCTime.ToString());
                Console.WriteLine("Page File           : {0}", fPageFile.ToString());
                Console.WriteLine("ProcessorQueueLengh : {0}  ", fProcessorQueueLengh.ToString());
                Console.WriteLine("Mem Available       : {0} MB", fMemAvailable.ToString());
                Console.WriteLine("Mem Committed       : {0} MB", fgetMemCommitted.ToString());
                Console.WriteLine("Mem Cached          : {0} MB", fgetMemCachedBytes.ToString());
                Console.WriteLine("Total Phys Memory   : {0} MB", memTot);
                Console.WriteLine("Avg.Disk queue Lengh: {0}", fDiskQueueLengh.ToString());
                Console.WriteLine("Net TrafficSent     : {0} Byte/s", fNetSent.ToString());
                Console.WriteLine("Net TrafficRcv      : {0} Byte/s", fNetReceived.ToString());
                Console.WriteLine("--------------------------------------------------------");
                Thread.Sleep(3000);
            }
        }
    }
}
