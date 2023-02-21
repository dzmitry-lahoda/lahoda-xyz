﻿using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections;

namespace NRegFreeCom
{

    public class UserRegAsm :RegAsm, IRegAsm
    {


        public void RegisterLocalServer(Type t)
        {

        }


        public void UnregisterLocalServer(Type t)
        {

        }
 
        public void RegisterInProcServer(Type t,RegistryView registryView = RegistryView.Default)
        {
            var reg = ComClrInfoFactory.CreateClass(t);
            #if NET35
            throw new NotImplementedException("Need to backport 4.0 methods");
#else
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView);
            var classes = root.CreateSubKey(CLASSES);
            registerInProcServer(classes, reg);
#endif
        }

        public void UnregisterInProcServer(Type t,RegistryView registryView = RegistryView.Default)
        {
            var reg = ComClrInfoFactory.CreateClass(t);
            #if NET35
            throw new NotImplementedException("Need to backport 4.0 methods");
#else
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView);
            var classes = root.CreateSubKey(CLASSES);
            unregisterInProcServer(classes, reg);
#endif
        }

        public void RegisterRecord(Type type, RegistryView registryView = RegistryView.Default)
        {
	throw new NotImplementedException();

        }
    	
		public void RegisterInterface(Type type, RegistryView registryView)
		{
            var reg = ComClrInfoFactory.CreateInterface(type);
            #if NET35
            throw new NotImplementedException("Need to backport 4.0 methods");
#else
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView);
            var classes = root.CreateSubKey(CLASSES);
            registerInterface(classes, reg);
#endif
		}



        public void RegisterTypeLib(System.Reflection.Assembly typeLib, RegistryView registryView )
		{
            var reg = ComClrInfoFactory.CreateTypeLib(typeLib);
            #if NET35
            throw new NotImplementedException("Need to backport 4.0 methods");
#else
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView);
            var classes = root.CreateSubKey(CLASSES);
            registerTypeLib(classes, reg);
#endif
		}

        public void UnregisterInterface(Type type, RegistryView registryView = RegistryView.Default)
        {
            var reg = ComClrInfoFactory.CreateInterface(type);
#if NET35
            throw new NotImplementedException("Need to backport 4.0 methods");
#else
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView);
            var classes = root.CreateSubKey(CLASSES);
            unregisterInterface(classes, reg);
#endif
        }

      

        public void UnregisterTypeLib(System.Reflection.Assembly typeLib, RegistryView registryView = RegistryView.Default)
        {
            var reg = ComClrInfoFactory.CreateTypeLib(typeLib);
#if NET35
            throw new NotImplementedException("Need to backport 4.0 methods");
#else
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView);
            var classes = root.CreateSubKey(CLASSES);
            unregisterTypeLib(classes, reg);
#endif
        }

     
    	
		public void RegisterInterfaces(IEnumerable<Type> types, RegistryView registryView)
		{
			var regs = new List<ComInterfaceInfo>();
			foreach (var type in types) {
				regs.Add( ComClrInfoFactory.CreateInterface(type));
			}
        
            #if NET35
            throw new NotImplementedException("Need to backport 4.0 methods");
#else
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, registryView);
            var classes = root.CreateSubKey(CLASSES);
            registerInterfaces(classes, regs);
#endif
		}
    }
}