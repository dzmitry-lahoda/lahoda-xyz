using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;



namespace nautoit.sample
{
    class Program
    {
    	
        /// testing calls in C# into AutoItX
        static void Main(string[] args)
        {
        	// AutoItX COM guid
            var clsid = new Guid("{1A671297-FA74-4422-80FA-6C5D8CE4DE04}");
            Console.WriteLine("Is64BitProcess={0}",Environment.Is64BitProcess);
            Console.WriteLine("Choose way to use auto it:");
        	Console.WriteLine("1 : Reg Free Com Manifest");
        	Console.WriteLine("2 : Direct creation via PInvoke of COM factory(no COM marshaling)");
        	Console.WriteLine("3 : Using official AutoItX.Assembly.dll layer aboce native AutoItX function");
        	Console.WriteLine();
        	Console.WriteLine("Write any number");
        	var k = Console.ReadKey();
        	Console.WriteLine();
        	switch (k.Key)
        	{
        	case ConsoleKey.D1:
        			{
        		// creates AutoItX using manifest instead of registry
                // needs manifest and AutoItX dll located in the same folder
            	//	needs AutoItX has the same version as in manifest
            	var manifest = "AutoItX3Dependency.manifest";
        		if (Environment.Is64BitProcess)
        			 manifest = "AutoItX3_x64Dependency.manifest";
        		  Console.WriteLine("Creating COM class with {0} id via {1}",clsid,manifest);
            	   var createdViaManifest = NRegFreeCom.ActivationContext.CreateInstanceWithManifest(clsid, manifest);
                   var autoItWithManifest = (AutoItX3.Interop.IAutoItX3)createdViaManifest;
                   autoItWithManifest.Init();
                   // next fails in DEBUG|x64 for unknown reason
                   Console.WriteLine("Using AutoItX COM '{0}' version",autoItWithManifest.version);
                   int retval = autoItWithManifest.Run("Notepad.exe",Environment.CurrentDirectory,autoItWithManifest.SW_MAXIMIZE);
                   Console.WriteLine("retval={0},error={0}",retval,autoItWithManifest.error);
        			}
        			break;
        	case ConsoleKey.D2:
        			{		
        				var dllName = "AutoItX3.dll";
        	 		if (Environment.Is64BitProcess)
        			 dllName = "AutoItX3_x64.dll";
        		   var assemblies = new NRegFreeCom.AssemblySystem();
                  var module = assemblies.LoadFrom(Path.Combine(Environment.CurrentDirectory,dllName ));
                   var createdDirectly = NRegFreeCom.ActivationContext.DangerousCreateInstanceDirectly(module, clsid) as AutoItX3.Interop.IAutoItX3;
                   createdDirectly.Init();
                       // next fails in DEBUG|x64 for unknown reason
                   Console.WriteLine("Using AutoItX COM '{0}' version",createdDirectly.version);
                   int retval = createdDirectly.Run("Notepad.exe",Environment.CurrentDirectory,createdDirectly.SW_MAXIMIZE);
                   Console.WriteLine("retval={0},error={0}",retval,createdDirectly.error);
        			}
        			break;
        case ConsoleKey.D3:
        			{		
        				
        				AutoIt.AutoItX.Init();            
                        int retval = AutoIt.AutoItX.Run("Notepad.exe",Environment.CurrentDirectory,AutoIt.AutoItX.SW_MAXIMIZE);
                       Console.WriteLine("retval={0},error={0}",retval,AutoIt.AutoItX.ErrorCode());
        			}
        			break;	
        		
        	default:
        		Console.WriteLine("Please write down known number. '{0}' is unkown",k.Key);
        		break;
        	}
        
        	Console.WriteLine("Press any key to exit");
        	Console.ReadKey();





        }
    }

}
