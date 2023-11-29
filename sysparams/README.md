<properties
pageTitle= 'Windows performance counters in .NET'
description= "Windows performance counters in .NET"
documentationcenter: github
services=""
documentationCenter="na"
authors="fabferri"
editor=""/>

<tags
   ms.service="performance system counters"
   ms.devlang="na"
   ms.topic="article"
   ms.tgt_pltfrm="na"
   ms.workload="na"
   ms.date="29/11/2023"
   ms.author="fabferri" />

# How to track Windows performance counters in .NET

In nuget is availabile a package **System.Diagnostics.PerformanceCounter** that allows capture of the system performance counters in Windows: [nuget PerformanceCounter package][link1] <br>
* The package provides an interface for collecting various kinds of system data such as CPU, memory, and disk usage. <br>
* The nuget package support .NET 7 and .NET 8 <br>
* The package version 8.0.0 has been verified.<br>

The **System.Diagnostics** namespace provides classes that allow you to interact with performance counters. The PerformanceCounter Class has different constructors; in our code we use the constructor with the following format:
```csharp 
public PerformanceCounter(
	string categoryName,
	string counterName,
	string instanceName
)
```
where:
* categoryName: it is the name of the performance counter category (performance object) with which this performance counter is associated. 
* counterName: The name of the performance counter. 
* instanceName: The name of the performance counter category instance, or an empty string (""), if the category contains a single instance. 

This constructor initializes the performance counter and associates the instance with an existing counter on the local computer. The values that you pass in for the **CategoryName**, **CounterName**, and **InstanceName** properties must point to an existing performance counter on the local computer.
Below some performance counters to track processor utilization, disk I/O, Memory, and networking:
```csharp
PerformanceCounter("Processor", "% Processor Time", "_Total");
PerformanceCounter("Processor", "% Privileged Time", "_Total");
PerformanceCounter("Processor", "% Interrupt Time", "_Total");
PerformanceCounter("Processor", "% DPC Time", "_Total");
PerformanceCounter("Memory", "Available MBytes", null);
PerformanceCounter("Memory", "Committed Bytes", null);
PerformanceCounter("Memory", "Commit Limit", null);
PerformanceCounter("Memory", "% Committed Bytes In Use", null);
PerformanceCounter("Memory", "Pool Paged Bytes", null);
PerformanceCounter("Memory", "Pool Nonpaged Bytes", null);
PerformanceCounter("Memory", "Cache Bytes", null);
PerformanceCounter("Paging File", "% Usage", "_Total");
PerformanceCounter("PhysicalDisk", "Avg. Disk Queue Length", "_Total");
PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Read", "_Total");
PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Write", "_Total");
PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
PerformanceCounter("Process", "Handle Count", "_Total");
PerformanceCounter("Process", "Thread Count", "_Total");
PerformanceCounter("System", "Context Switches/sec", null);
PerformanceCounter("System", "System Calls/sec", null);
PerformanceCounter("System", "Processor Queue Length", null);
```


> [!NOTE] <br>
> The Microsoft package **System.Diagnostics.PerformanceCounter** is only supported on Windows operating systems.
>

To add the **System.Diagnostics.PerformanceCounter** package to the Visual Studio project:

[![1]][1]

[![2]][2]

Running the program will be shown in the console the the following parameters:

[![3]][3]

## <a name="CategoryName: Processor"></a>1. CategoryName: Processor

**PerformanceCounter("Processor", "% Processor Time", "_Total");** <br>
The **Processor\% Processor Time** counter determines the percentage of time the processor is busy by measuring the percentage of time the thread of the Idle process is running and then subtracting that from 100 percent. This measurement is the amount of processor utilization.
<br>

**PerformanceCounter("Processor", "% Interrupt Time", "_Total");** <br>
The rate, in average number of interrupts in incidents per second, at which the processor received and serviced hardware interrupts. It does not include deferred procedure calls, which are counted separately.
<br>

**PerformanceCounter("Processor", "% DPC Time", "_Total");** <br>
The percentage of time that the processor spent receiving and servicing deferred procedure calls during the sample interval. Deferred procedure calls are interrupts that run at a lower priority than standard interrupts.
<br>

**PerformanceCounter("Processor", "% Privileged Time", "_Total");**
The percentage of non-idle processor time spent in privileged mode. Privileged mode is a processing mode designed for operating system components and hardware-manipulating drivers. It allows direct access to hardware and all memory. The alternative, user mode, is a restricted processing mode designed for applications, environment subsystems, and integral subsystems. The operating system switches application threads to privileged mode to gain access to operating system services. This includes time spent servicing interrupts and deferred procedure calls (DPCs). A high rate of privileged time might be caused by a large number of interrupts generated by a failing device. This counter displays the average busy time as a percentage of the sample time.



## <a name="CategoryName: Memory"></a>2. CategoryName: Memory

**PerformanceCounter("Memory", "Available MBytes", null);** <br>
This measures the amount of physical memory, in megabytes, available for running processes. If this value is less than 5 percent of the total physical RAM, that means there is insufficient memory, and that can increase paging activity. 
<br>

**PerformanceCounter("Memory", "Committed Bytes", null);** <br>
Shows the amount of virtual memory, in bytes, that can be committed without having to extend the paging file(s). Committed memory is physical memory which has space reserved on the disk paging files. There can be one or more paging files on each physical drive. If the paging file(s) are expanded, this limit increases accordingly.

**PerformanceCounter("Memory", "Commit Limit", null);** <br>
Shows the amount of virtual memory, in bytes, that can be committed without having to extend the paging file(s). Committed memory is physical memory which has space reserved on the disk paging files. There can be one or more paging files on each physical drive. If the paging file(s) are expanded, this limit increases accordingly.

**PerformanceCounter("Memory", "% Committed Bytes In Use", null);** <br>
shows the ratio of Memory\ Committed Bytes to the Memory\ Commit Limit. Committed memory is physical memory in use for which space has been reserved in the paging file so that it can be written to disk. The commit limit is determined by the size of the paging file. If the paging file is enlarged, the commit limit increases, and the ratio is reduced.

**PerformanceCounter("Memory", "Pool Paged Bytes", null);** <br>
Shows the size, in bytes, of the paged pool. Memory\ Pool Paged Bytes is calculated differently than Process\ Pool Paged Bytes, so it might not equal Process(_Total )\ Pool Paged Bytes.

**PerformanceCounter("Memory", "Pool Nonpaged Bytes", null);** <br>
Shows the size, in bytes, of the nonpaged pool. Memory\ Pool Nonpaged Bytes is calculated differently than Process\ Pool Nonpaged Bytes, so it might not equal Process(_Total )\ Pool Nonpaged Bytes.

**PerformanceCounter("Memory", "Cache Bytes", null);**
Shows the sum of the values of System Cache Resident Bytes, System Driver Resident Bytes, System Code Resident Bytes, and Pool Paged Resident Bytes.

## <a name="CategoryName: Memory"></a>3. CateroryName: PhysicalDisk 

**PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");** <br>
**PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");** <br>
PerformanceCounter captures the total number of bytes sent to the disk (write) and retrieved from the disk (read) during write or read operations.
<br>

**PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Read", "_Total");** <br>
**PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Write", "_Total");** <br>
it captures the average time, in seconds, of a read/write of data from/to the disk.


#### <a name="CategoryName: Memory"></a>4. CategoryName: System

**PerformanceCounter("System", "Context Switches/sec", null);** <br>
A context switch occurs when the kernel switches the processor from one thread to another. A context switch might also occur when a thread with a higher priority than the running thread becomes ready or when a running thread must wait for some reason (such as an I/O operation). Context switching activity is important for several reasons. A program that monopolizes the processor lowers the rate of context switches because it does not allow much processor time for the other processes' threads. A high rate of context switching means that the processor is being shared repeatedly — for example, by many threads of equal priority. The Thread\Context Switches/sec counter value increases when the thread gets or loses the time of the processor. A high context-switch rate often indicates that there are too many threads competing for the processors on the system. The **System\Context Switches/sec** counter reports systemwide context switches.
<br>

**PerformanceCounter("System", "System Calls/sec", null);** <br>
This is the number of system calls being serviced by the CPU per second. By comparing the **Processor's Interrupts/sec** with the **System Calls/sec** we can get a picture of how much effort the system requires to respond to attached hardware. On a healthy system, the Interrupts per second should be negligible in comparison to the number of System Calls per second. When the system has to repeatedly call interrupts for service, it's indicative of a hardware failure.
<br>

**PerformanceCounter("System", "Processor Queue Length", null);** <br>
Note that regardless of the number processors, there will always be only one processor queue. This counter measures the number of threads in the processor queue. It is the number of threads  aiting on the processor to be served.

<br>

`Tags: .NET, Performance Counters` <br>
`date: 29-11-23`

<!--Image References-->


[1]: ./media/nuget1.png "adding the nuget package to the Virtual Studio project"
[2]: ./media/nuget2.png "dependencies"
[3]: ./media/monitor.png "Windows performance counters"

<!--Link References-->

[link1]: https://www.nuget.org/packages/System.Diagnostics.PerformanceCounter/
