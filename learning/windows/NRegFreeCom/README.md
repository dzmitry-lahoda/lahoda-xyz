## NRegFreeCom

 Load native libraries, call native functions, do COM objects without registration, do COM registrations in runtime, in .NET

 COM is good for native interop and easy IPC, but lacks clean coded way to do such interaction without deployment/compile time registry in .NET.

 Managed code is XCOPY on 32/64. This lib helps making native interlop also XCOPY by using pattern of deploying both versions of native libs and deciding in runtime, like OS libraries PInvokes work. 

 This project contains samples and reusable patters of things above.

## Content
* PInvokes used to work with native libraries and reg free COM objects
* Sample how to load native library with module definition as assembly and providing it with managed COM services
* Helpers to work with native and COM threading
* Sample of native library implementing managed COM interface searched and  instantiated by managed code using manifest
* Managed out of process COM server which uses Running Object Table to broadcast it instance (zero Windows Registry info)
* Managed out of process COM server which does registration at start and unregisters when closing
* Working with native library dependencies and resources

## Build and deployment

* `NRegFreeCom` is redistributable managed library, can be build in Release and Debug mode Visual Studio 2017. Is build for `Any Cpu`
* Library testing is done on XP SP3 and Windows 7 SP2
* Run `deploy.bat`


### Samples build

* Visual Studio 2017 with Visual C++ and MFC
* Debug|Mixed Platforms
* Windows 10
* Run Visual Studio as Admin

## Samples

```csharp

       [Test]
		[Description("Creates and calls managed registration free object")]
        public void CreateInstanceWithManifest_inProcessManagedServer_OK()
        {		    
            var path = Path.Combine(Environment.CurrentDirectory, @"RegFreeCom.Implementations.dll.manifest");
            var guid = new Guid(RegFreeComIds.CLSID);

            var obj = ActivationContext.CreateInstanceWithManifest(guid, path);
            var inf = (IRegFreeCom)obj;
            var result = inf.Answer();
			
            Assert.IsTrue(result == 42);
        }
		
		[Test]
        [Description("Loads library from subfolder with dependencies searched in this subfolder")]
        public void TestDllDependenciesLoading()
        {
            var loader = new AssemblySystem();

            // C# loads C++ from Win32 or x64 subfolder
            var anyCpu = loader.GetAnyCpuPath(loader.BaseDirectory);
            loader.AddSearchPath(anyCpu);
            var module = loader.LoadFrom(anyCpu, "NativeLibraryConsumer.dll");
            var fn = module.GetDelegate<fnNativeLibraryConsumer>();
            object[] retval;

            Assert.IsTrue(42 == fn(out retval));
            loader.Dispose();
        }
		
        [Test]
        [Description("Tests basic workflow of custom Dispatcher")]
        public void GetAndRunDispatcher_invokeAndShutdown_invocationInDispatcherDone()
        {
            //start custom Dispatcher for Windows Message Pump
            NRegFreeCom.IDispatcher disp = null;
            var created = new ManualResetEvent(false);
            var t = new Thread(x =>
            {
                disp = NRegFreeCom.Dispatcher.CurrentDispatcher;
                created.Set();
                NRegFreeCom.Dispatcher.Run();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            created.WaitOne();

            // run custom Dispatcher
            int threadId = -1;
            bool wasAct = false;
            Action act = () =>
            {
                wasAct = true;
                threadId = Thread.CurrentThread.ManagedThreadId;
            };
            disp.Invoke(act);
            disp.InvokeShutdown();

            // invocation in Dispatcher thread was done
            Assert.AreEqual(threadId, t.ManagedThreadId);
            Assert.That(wasAct, Is.True);
        }

    [TestFixture]
    public class RegAsmTests
    {
        [Test]
        public void User_RegisterInProcSever()
        {
            var type = typeof (RuntimeRegServer);
            RegAsm.User.RegisterInProcSever(type);
        }

        [Test]
        public void User_UnregisterInProcSever()
        {
            var type = typeof(RuntimeRegServer);
            RegAsm.User.UnregisterInProcSever(type);
        }
    }

	        [Test]
        public void Create_oneKeyWithOneValue_OK()
        {
            var text = 
@"Windows Registry Editor Version 5.00
[HKEY_LOCAL_MACHINE\SOFTWARE\MyKey]
" +
"\"MyValue\"=\"_RegFreeComRotClass\"";
            var stream = stringToStream(text,Encoding.UTF8);
            var reader = new RegFileReader(stream);
            var key = reader.RegValues[@"HKEY_LOCAL_MACHINE\SOFTWARE\MyKey"];
            Assert.IsNotNull(key);
            Assert.IsTrue(key.ContainsKey("MyValue"));
        }
```

See tests and samples in code for other functional (like inter process communication without server Windows registry entry; loading and initializing native libraries, methods and COM objects).

##Notes

[Guide to COM activation and use in relation to registration ](doc/regfreecomguide.md)

Project design and reasoning:

[Isolation and integration : native vs. managed](doc/isolateintegrate.md)

[What makes C# .NET ready for developing Windows desktop applications](doc/netnative.md)

[Guide to ease integration of C++ and C#](doc/nativemanagedeasy.md)


## TODO:
* automate Nuget build (using FAKE)
* generate reg file during COM registrations
* add support for Record and dependency Class-Inteface Class-TypeLib Interface-TypeLib registration
* add assembly batch registration methods RegisterTypeLibWithInterfaces, next is slow (	foreach (var type in ComTypes.ComInterfaces) {ComClrInfoFactory.CreateInterface(type);}
* need to check guess that IDispEventImpl needs registry, but  IDispEventSimpleImpl - not, document this
* avoid direct dependency on assembly to allow assembly touchless registration (e.g. if metadata was read out via Mono.Cecil)
* add rgs parser, add xml with semantics of rgs, reg-xml-rgs translator
* add regasm options (unregister all version or only this, register type interfaces or not)
* todo add method to transform positive reg file to negative (add - to remove all keys)
* Enumerate DLL exports
* Make custom resources sample and read this.
* Try to do with resources what WinRT did. Embeed CLI metadata describing reg free COM component, generate manifest from it, and create COM, use Metadata Interfaces. http://msdn.microsoft.com/en-us/library/ms233411.aspx.
* Make delegates for all standards DEFs( DLL, COM)
* Fake registration out of process ROT client instead of using proxy
* Add dependency conflicts for managed and for native and resolve both by SxS manifests tests
* Imitate AppDomains based on runtime binding
* Add PE code (detecting managed headers, DEF and COM headers).
* Research how  api-ms-win-core can be employed for isolation http://www.nirsoft.net/articles/windows_7_kernel_architecture_changes.html
* User free tools. Use SharpDevelop to build all C#. Use NMAKE/CL/MIDL and/or C++ template of SD to build all C++. Compile C++/C# in runtime and start processes to make tests more robust.
* Investigate WiX and Import/Export reg to Xml projects which contain reg parsing and to XML code, drop support here or reuse then
* Check Mono and write COM activation if needed for it
* Migrate to CMAKE and CLANG
* Write pure managed COM
* Consider DLL loading proposal in corefx and drop code wich is same here or unify

##Q&A

Q: If I want to load some DLLs of which are there inside "Bin" folder
A:
If just dll then 
```csharp
            var bin = Path.GetFullPath("Bin");
            var dlls = new NRegFreeCom.AssemblySystem();
            dlls.AddSearchPath(bin);
            var dll = dlls.LoadFrom(Path.Combine(bin, "MyDll.dll"));
```
