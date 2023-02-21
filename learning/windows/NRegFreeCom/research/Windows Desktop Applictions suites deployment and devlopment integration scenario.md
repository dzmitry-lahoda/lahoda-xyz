

Desktop Application Suite (Suite) - is collection of stand alone applications and addins (e.g. to MS Office or Windows Explorer) with internal host extensibility defined (custom add-ins, plugins) and custom components ready to be reused.


Context:

- Integration of Suite1 and Suite2:

- Suite2 reuses parts of Suite1

- Suite1 and Suite2 located in different directories


Problem:

Need to support next scenarios:

- Suite1 as standalone

- Suite2 as standalone

- Suite1 upgraded with Suite2

- Suite2 upgraded with Suite1


Solution:


Solutions are in organization and technical space


How stuff will be shared in runtime:


- shared confurations/schemas in registy or files

- shared executable code dll

- shared IPC protocol, starting exe or using existing Windows service to start chatting


How to label and managed integrations points[1]: 


- shared code development (Shared Kernel)

- what relationships are when Suite2 uses Suite1 parts  (Customer supplier, Confimist)

- should Suite2 isolate some parts of Suite1 for own add-ins (Anticorruption Layer)

- should some reimplementaion involved (Separate Ways), e.g. C++ code rewritten in C#

- few dlls of Suite2 is pluged into Suite1 as hook to do IPC later (Open Host Service)


How to make effortless compatibility effective via transparency and traceability of Suite1's reused parts:

- searchable release notes for each reused part

- improved reusabilty refactoring basing on some principles [2]

- enfoced separation of reused codebase into other code repository other then Suite1 located (enforeces modularization)

- sample like Suite2  which uses Suite1 in the same way and with similar dependcyies size

- all dependecies provided by Suite1 or used by Suite1 are published on premice or pulic Nuget for total versions traceability

- choose common API guidleines for different parts (C api like WinApi, interface based COM api, C++ headers, runtime shared schema or protocol of configuration or IPC)


Seam points:

- identify most stables libraries linked only dynamically which can be duplicated without conflicts in user session

- identify if Suite1 and/or Suite2 can dissociate custom hosting logic, so that always go both together but modules loaded in runtime differ in deployment

- define integration interfaces and singletons to put into separate dll to be published and reused by any suite

- defined version which expresses comatiblity of suite versions supported. E.g. coversion Suite1 and Suite2 by major number like 3.1 Suite1 and 3.22 Suite2 are compatible, but 3.1 and 2.1 are not.

- provide 2 installers contain duplicated dlls which do install in the same folder with check of compatibility


How dll dependencies loaded:

- using COM SxS (loads dependencies from manifest location), AddDllDirectory, SetDefaultDllDirectories and related for native code

- using AppDomain.AssemblyResolve and related for managed



[1]: http://www.infoq.com/resource/minibooks/domain-driven-design-quickly/en/pdf/DomainDrivenDesignQuicklyOnline.pdf Preserving Model Integrity

[2]: http://www.objectmentor.com/resources/articles/Principles_and_Patterns.pdf