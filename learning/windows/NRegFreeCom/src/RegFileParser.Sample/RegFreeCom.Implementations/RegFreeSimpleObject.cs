﻿/****************************** Module Header ******************************\
* Module Name:  SimpleObject.cs
* Project:      CSExeCOMServer
* Copyright (c) Microsoft Corporation.
* 
* The definition of the COM class, SimpleObject, and its ClassFactory, 
* SimpleObjectClassFactory.
* 
* (Please generate new GUIDs when you are writing your own COM server) 
* Program ID: CSExeCOMServer.SimpleObject
* CLSID_SimpleObject: DB9935C1-19C5-4ed2-ADD2-9A57E19F53A3
* IID_ISimpleObject: 941D219B-7601-4375-B68A-61E23A4C8425
* DIID_ISimpleObjectEvents: 014C067E-660D-4d20-9952-CD973CE50436
* 
* Properties:
* // With both get and set accessor methods
* float FloatProperty
* 
* Methods:
* // HelloWorld returns a string "HelloWorld"
* string HelloWorld();
* // GetProcessThreadID outputs the running process ID and thread ID
* void GetProcessThreadID(out uint processId, out uint threadId);
* 
* Events:
* // FloatPropertyChanging is fired before new value is set to the 
* // FloatProperty property. The Cancel parameter allows the client to cancel 
* // the change of FloatProperty.
* void FloatPropertyChanging(float NewValue, ref bool Cancel);
* 
\***************************************************************************/


using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using NRegFreeCom;
using NRegFreeCom.Interop;
using RegFreeCom.Interfaces;

namespace RegFreeCom.Implementations
{



    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComSourceInterfaces(typeof(ISimpleObjectEvents))]
    [Guid("EC49F901-86F7-4891-BC94-CC83C6ABC009")]
    [ComDefaultInterface(typeof(ISimpleObject))]
    [ComVisible(true)]
    public class RegFreeSimpleObject : ISimpleObject
    {


        private float fField = 0;


        public float FloatProperty
        {
            get
            {
     
               
                return this.fField;

            }
            set
            {
                bool cancel = false;
                // Raise the event FloatPropertyChanging
                if (null != FloatPropertyChanging)
                    FloatPropertyChanging(value, ref cancel);
                if (Callbacks != null)
                {
                    Callbacks.FloatPropertyChanging(value, ref cancel);
                }
                if (!cancel)
                    this.fField = value;
            }
        }

        public string Info
        {
            get
            {
                return
                    string.Format("Type:{0}, ProcessName:{1}, ProcessId:{2},ThreadId:{3}",
                   this.GetType().FullName, Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId);
            }
        }

        public void RaisePassStruct()
        {
            var val = new MyCoolStuct { _Val = 123, _Val2 = "312" };
            if (Callbacks != null)
            {
                Callbacks.PassStuct(val);
            }
            if (PassStuct != null)
            {
                PassStuct(val);
            }

        }

        public ISimpleObjectEvents Callbacks { get; set; }

        private IMyCoolClass _obj;
        public void RaisePassClass()
        {
            _obj = new MyCoolClass();
            if (Callbacks != null)
            {
                Callbacks.PassClass(_obj);
            }
            if (PassClass != null)
            {
                PassClass(_obj);
            }
        }

        public void RaisePassString()
        {
            if (Callbacks != null)
            {
                Callbacks.PassString("Hello from managed");
            }
            if (PassString != null)
            {
                PassString("Hello from managed");
            }
        }

        public void RaiseEnsureGCIsNotObstacle()
        {
            if (Callbacks != null)
            {
                Callbacks.EnsureGCIsNotObstacle();
            }
            if (EnsureGCIsNotObstacle != null)
            {
                EnsureGCIsNotObstacle();
            }
        }

        public void RaiseEmptyEvent()
        {
            if (Callbacks != null)
            {
                Callbacks.SimpleEmptyEvent();
            }
            if (SimpleEmptyEvent != null)
            {
                SimpleEmptyEvent();
            }
        }

        public string HelloWorld()
        {
            return "HelloWorld" + fField;
        }

        public void GetProcessThreadID(out uint processId, out uint threadId)
        {
            processId = NativeMethods.GetCurrentProcessId();
            threadId = NativeMethods.GetCurrentThreadId();
        }




        [ComVisible(false)]
        public delegate void PassStuctEventHandler(MyCoolStuct val);

        public event PassStuctEventHandler PassStuct;

        [ComVisible(false)]
        public delegate void PassClassEventHandler(IMyCoolClass obj);
        public event PassClassEventHandler PassClass;

        [ComVisible(false)]
        public delegate void PassStringEventHandler(string str);
        public event PassStringEventHandler PassString;

        [ComVisible(false)]
        public delegate void FloatPropertyChangingEventHandler(float NewValue, ref bool Cancel);
        public event FloatPropertyChangingEventHandler FloatPropertyChanging;

        [ComVisible(false)]
        public delegate void EnsureGCIsNotObstacleEventHandler();
        public event EnsureGCIsNotObstacleEventHandler EnsureGCIsNotObstacle;
        [ComVisible(false)]
        public delegate void SimpleEmptyEventEventHandler();
        public event SimpleEmptyEventEventHandler SimpleEmptyEvent;
    }
}
