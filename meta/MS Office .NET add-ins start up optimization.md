# Avoid isolation when not needed

VSTO and COM Shim provide modularity with isolation via AppDomains.

If several add-in are written by same company using mentioned tools, each add-in employees new AppDomain. 

Loading AppDomain involves loading assemblies and JIT, which matters.


Use one VSTO or COM Shim add-in with custom modulary with optional isolation. Instead having several add-in, create one add-in which hosts several modules into the same AppDomain. 

Additional bonus is more flexibility in modules registration/activation/loading.


# Logging

Logging initialization and writes hampers start up time, adds resource contention on start.


By default, no logs should be written on start, until this are real Errors or Warnings. So everyday start of MS Office should write no logs until something bad happens.

Use delayed formatting of log entry (i.e. check if can log before creating string for logging which pressures GC).


Reduce number of assemblies loaded on start up


Assemblies loading takes time, more time if there is to many dlls. [1] 

CLR Inside Out Improving Application Startup Time[2]:


    In terms of the CPU, assembly loads have fusion binding and CLR assembly-loading overhead in addition to the LoadLibrary call, so fewer modules mean less CPU time. In terms of memory usage, fewer assemblies also mean that the CLR will have less state to maintain.

Reduce number of projects in solution, 

il-repack 3rd party assemblies always used together.

Organize code with namespaces until creating assemby proved to add value for some reason.


# Lazy initialization


Try not load or initialize or execute what is not needed during start up. 

Initialize/load on demand (e.g. users clicks ribbon button -  initialize WPF and load related assemblies) or delay execution (prevents resource contention during start up).


# Discovery and composition of object graph

Measure and optimize time consumed by discovery and composition framework in solution. Consider what fits best module needs and what features used and what good practices are. [3] [4] [5]


# Ribbon

Invalidate separate controls when needed. Not whole ribbon. Whole ribbon invalidating leads to invoking numerous ribbon callbacks of other add-ins. Having big codebase with calls to Invalidate, use API that delays invalidation until timer ticks.


# COM UI thread vs. any other code separation

Time of main MS Office thread(STA COM thread) is very valuable. Everything that takes time of STA COM thread should be explicit and separated, e.g. calling MS Office API from initializing cache service. Minimal amount of work should be done on STA COM thread and only if it really needs accessing COM.



# Threading

Use thread pool, timers and thread with reduced stacks when possible. Default thread's constructor produces thread with stack of 1 MB size -  needs noticeable time for allocation, consumes memory after.

Do not create to much threads which will not speed up nothing (vice versa is more probable), considering that user does not have >=8 hardware threads processor and solid state drive. Consider patters like producer consumer queue.



# See aslo [6]


Projects compiled to  assemblies

Create new projects(assemblies) only if they should

- be loaded in different AppDomains/Executables

- be shared/being part of contract with 3rd party which has own source repository

- loaded and initialized on demand

Use namespaces to divide components/modules/features when possible instead of assemblies. Control dependencies (prevent cycles) manually.



# How to reduce resource usage after login:

    Reduce number of dlls loaded clever way, lazy load dlls

    Search for reuse in code using Object Oriented Analysis and Design (OOAD) and OOP on every day basis, people writing code should know how to search for reuse and what OOAD is.

    Divide Excel functional and Daemon functional better ending to be located in different dlls.  for .NET AppDomain separation equals Process separation, so divide what is used in AppDomains.


[1] http://stackoverflow.com/questions/1192004/specific-down-sides-to-many-small-assemblies

[2] http://msdn.microsoft.com/en-us/magazine/cc163655.aspx

[3] http://www.iocbattle.com/

[4] http://www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison

[5] http://philipm.at/2011/di_speed_redux.html#tldr

[6] http://blogs.msdn.com/b/jgoldb/archive/2007/10/10/improving-wpf-applications-startup-time.aspx