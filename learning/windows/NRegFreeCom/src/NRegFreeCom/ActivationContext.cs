﻿using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using NRegFreeCom.Interop;
using NRegFreeCom.Interop.ComTypes;

namespace NRegFreeCom
{
    ///<summary>
    /// Used tune activation context stack of thread for intializing and loading SxS components.
    /// </summary>
    ///<seealso cref="Microsoft.Windows.ActCtx"/>
    ///<seealso href="http://www.atalasoft.com/blogs/spikemclarty/february-2012/dynamically-testing-an-activex-control-from-c-and"/>
    public class ActivationContext
    {

        public delegate void doSomething();

        /// <summary>
        /// Create an instance of a COM object given the GUID of its class
        /// and a filepath of a client manifest (AKA an application manifest.)
        /// </summary>
        /// <param name="guid">GUID = CLSID of the COM object, {NNNN...NNN}</param>
        /// <param name="manifest">full path of manifest to activate, should list the
        /// desired COM class as a dependentAssembly.</param>
        /// <returns>An instance of the specified COM class, or null.</returns>
        static public object CreateInstanceWithManifest(Guid guid, string manifest)
        {
            object comob = null;
            ActivationContext.UsingManifestDo(manifest, delegate()
                {
                    // Get the type object associated with the CLSID.
                    Type T = Type.GetTypeFromCLSID(guid);
                    
                    // Create an instance of the type:
                    comob = System.Activator.CreateInstance(T);
                });
            return comob;
        }

   
        /// <summary>
        /// Applies content of <paramref name="pathToManifest"/> file to current context, invokes <paramref name="thingToDo"/> delegate, deactivates applied context.
        /// </summary>
        /// <param name="pathToManifest"></param>
        /// <param name="thingToDo"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void UsingManifestDo(string pathToManifest, doSomething thingToDo)
        {
            var context = new ACTCTX();
            context.cbSize = Marshal.SizeOf(typeof(ACTCTX));
            bool wrongContextStructure = (context.cbSize != 0x20 && IntPtr.Size == 4) // ensure stucture is right on 32 bits
                                  || (context.cbSize != 52 && IntPtr.Size == 8); // the same for 64 bits
            if (wrongContextStructure)
            {
                throw new Exception("ACTCTX.cbSize is wrong");
            }
            context.lpSource = pathToManifest;

            IntPtr hActCtx = NativeMethods.CreateActCtx(ref context);
            if (hActCtx == Constants.INVALID_HANDLE_VALUE)
            {
                var error = Marshal.GetLastWin32Error();
                if (error == SYSTEM_ERROR_CODES.ERROR_FILE_NOT_FOUND)
                {
                    throw new System.IO.FileNotFoundException("Failed to find manifest", pathToManifest);
                }
                throw new Win32Exception(error);
            }
            try // with valid hActCtx
            {
                IntPtr cookie = IntPtr.Zero;
                if (!NativeMethods.ActivateActCtx(hActCtx, out cookie))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                try // with activated context
                {
                    thingToDo();
                }
                finally
                {
                    NativeMethods.DeactivateActCtx(0, cookie);
                }
            }
            finally
            {
                NativeMethods.ReleaseActCtx(hActCtx);
            }
        }

        /// <summary>
        /// Given CLR assembly search manifest of it located in the same folder with .manifest suffix.
        /// Activates manifest found, invokes <paramref name="thingToDo"/> delegate, deactivates applied context. 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="action"></param>
        public static void UsingAssemblyManifestDo(System.Reflection.Assembly assembly, doSomething action)
        {
            var manifest = assembly.Location + ".manifest";
            UsingManifestDo(manifest, action);
        }

        /// <summary>
        /// Creates  COM class instance directly without using any COM Activation or thread Marshaling services.
        /// </summary>
        /// <param name="libraryModule"></param>
        /// <param name="clsid"></param>
        /// <returns></returns>
        public static object DangerousCreateInstanceDirectly(NRegFreeCom.IAssembly libraryModule, Guid clsid)
        {
            var classFactory = GetClassFactory(libraryModule, clsid);
            var iid = new Guid(WELL_KNOWN_IIDS.IID_IUnknown);
            object obj;
            classFactory.CreateInstance(IntPtr.Zero, ref iid, out obj);
            return obj;
        }

        /// <seealso href="https://gist.github.com/1568627/76da6e65a2d925fffdb48a0ae5efd783281f9244"/>
        private static IClassFactory_AutoMarshal GetClassFactory(NRegFreeCom.IAssembly libraryModule, Guid clsid)
        {
            var getClassFactory = libraryModule.GetDelegate<DEF_Objbase.DllGetClassObject>();
            var classFactoryIid = new Guid(WELL_KNOWN_IIDS.IID_IClassFactory);
            IClassFactory_AutoMarshal classFactory;
            var hresult = getClassFactory(ref clsid, ref classFactoryIid, out classFactory);
            if (hresult != 0)
            {
                throw new Win32Exception(hresult, string.Concat("Cannot create class factory for {0}", clsid));
            }
            return classFactory;
        }

    }
}