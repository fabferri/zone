using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace SysParams
{
    class PollingParams
    {
        public static PerformanceCounter cpuProcessorTime = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter cpuPrivilegedTime = new PerformanceCounter("Processor", "% Privileged Time", "_Total");
        public static PerformanceCounter cpuInterruptTime = new PerformanceCounter("Processor", "% Interrupt Time", "_Total");
        public static PerformanceCounter cpuDPCTime = new PerformanceCounter("Processor", "% DPC Time", "_Total");
        public static PerformanceCounter pageFile = new PerformanceCounter("Paging File", "% Usage", "_Total");
        public static PerformanceCounter processorQueueLengh = new PerformanceCounter("System", "Processor Queue Length", null);
        public static PerformanceCounter memAvailable = new PerformanceCounter("Memory", "Available MBytes", null);
        public static PerformanceCounter memCommited = new PerformanceCounter("Memory", "Committed Bytes", null);
        public static PerformanceCounter memCachedBytes = new PerformanceCounter("Memory", "Cache Bytes", null);
        public static PerformanceCounter diskQueueLengh = new PerformanceCounter("PhysicalDisk", "Avg. Disk Queue Length", "_Total");
        public static PerformanceCounterCategory performanceNet = new PerformanceCounterCategory("Network Interface");
        public static PerformanceCounter[] dataSentCounters;
        public static PerformanceCounter[] dataReceivedCounters;
        private static string[] interfaces = null;
        private static float sendSum = 0.0F;
        private static float receiveSum = 0.0F;

        public static void InitNetParams()
        {
            performanceNet = new PerformanceCounterCategory("Network Interface");
            interfaces = performanceNet.GetInstanceNames();

            int length = interfaces.Length;
            if (length > 0)
            {
                dataSentCounters = new PerformanceCounter[length];
                dataReceivedCounters = new PerformanceCounter[length];
            }

            for (int i = 0; i < length; i++)
            {
                // Initializes a new, read-only instance of the PerformanceCounter class.
                // 1st paramenter: "categoryName"-The name of the performance counter category (performance object) with which 
                //                   this performance counter is associated. 
                // 2nd paramenter: "CounterName" -The name of the performance counter. 
                // 3rd paramenter: "instanceName" -The name of the performance counter category instance, or an empty string (""), if the category contains a single instance. 
                dataReceivedCounters[i] = new PerformanceCounter("Network Interface", "Bytes Received/Sec", interfaces[i]);
                dataSentCounters[i] = new PerformanceCounter("Network Interface", "Bytes Sent/sec", interfaces[i]);
            }

            // List of all names of the network interfaces
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("Name netInterface: {0}", performanceNet.GetInstanceNames()[i]);
            }
        }

        public static float getTrafficSent()
        {
            int length = interfaces.Length;
            sendSum = 0.0F;

            for (int i = 0; i < length; i++)
            {
                sendSum += dataSentCounters[i].NextValue();
            }
            return sendSum;
        }
        public static float getTrafficReceived()
        {
            int length = interfaces.Length;
            receiveSum = 0.0F;

            for (int i = 0; i < length; i++)
            {
                receiveSum += dataReceivedCounters[i].NextValue();
            }
            return receiveSum;
        }

        public static ulong GetPhysicalMemory()
        {
            MEMORYSTATUSEX status = new MEMORYSTATUSEX();
            status.length = Marshal.SizeOf(status);
            if (!GlobalMemoryStatusEx(ref status))
            {
                int err = Marshal.GetLastWin32Error();
                throw new Win32Exception(err);
            }
            return status.totalPhys;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MEMORYSTATUSEX
        {
            public int length;
            public int memoryLoad;
            public ulong totalPhys;
            public ulong availPhys;
            public ulong totalPageFile;
            public ulong availPageFile;
            public ulong totalVirtual;
            public ulong availVirtual;
            public ulong availExtendedVirtual;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX buffer);
    }
}
