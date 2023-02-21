﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using RegFreeCom.Interfaces;

namespace RegFreeCom.Implementations
{
    [ComVisible(true)]
    [Guid("581FBD61-BD82-4E2A-8200-519D90BBB1F7")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComDefaultInterface(typeof(IRegFreeComRotClass))]
    public class RegFreeComRotClass : IRegFreeComRotClass
        , ICustomQueryInterface
    {
        public string Info
        {
            get
            {
                return
                    string.Format("Type:{0}, ProcessName:{1}, ProcessId:{2},ThreadId:{3}",
                   this.GetType().FullName, Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId);
            }
        }

        public void Request(string hello)
        {
            Console.WriteLine("Request was done with:{0}", hello);
        }

        public void Ping()
        {
            Console.WriteLine("Ping");
        }

        public int Answer()
        {
            return 42;
        }
        public ISimpleObject Create()
        {
            return new RegFreeSimpleObject();
        }

       // NOTE: is called and returned, but not helps with ROT calls with NO registration of interfaces.
        //NOTE: deleting registy after server running does not helps, mean client side issue.
        public CustomQueryInterfaceResult GetInterface(ref Guid iid, out IntPtr ppv)
        {
            if (new Guid(RotIds.IID) == iid)
            {
                ppv = Marshal.GetComInterfaceForObject(this, typeof (IRegFreeComRotClass),CustomQueryInterfaceMode.Ignore);
                return CustomQueryInterfaceResult.Handled;
            }
            ppv = IntPtr.Zero;
            return CustomQueryInterfaceResult.NotHandled;
        }
    }
}