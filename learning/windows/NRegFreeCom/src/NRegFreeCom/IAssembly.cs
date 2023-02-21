﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using NRegFreeCom.Interop;

namespace NRegFreeCom
{
    public interface IAssembly : IAssemblyInfo,IDisposable
    {
        string FullName { get; }

        string Location { get; }

        Stream LoadCompiledResource(uint id);

        Stream LoadResource(uint id, RESOURCE_TYPES type);

        /// <summary>
        /// Loads string table resources from assembly.
        /// </summary>
        /// <param name="id">The identifier of the string to be loaded.</param>
        /// <returns></returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms647486.aspx"/>
        string LoadStringTableResource(uint id);

        /// <summary>
        /// Gets public function from native library.
        /// </summary>
        /// <typeparam name="T">
        /// Managed counterpart delegate of native function. 
        /// If <paramref name="defName"/> not provided then <typeparamref name="T"/> <see cref="MemberInfo.Name"/> is used for search.
        /// </typeparam>
        /// <paramref name="defName">Optional name of function. If not defined then name of<typeparamref name="T"/> is used</paramref>
        /// <returns></returns>
        /// <exception cref="EntryPointNotFoundException">No such export in library</exception>
        T GetDelegate<T>(string defName = null)
            where T : class, ISerializable, ICloneable; // is delegate

        /// <summary>
        /// Tries to get public function from native library. Returns false if no function was found.
        /// </summary>
        /// <typeparam name="T">
        /// Managed counterpart delegate of native function. 
        /// If <paramref name="defName"/> not provided then <typeparamref name="T"/> <see cref="MemberInfo.Name"/> is used for search.
        /// </typeparam>
        /// <paramref name="defName">Optional name of function. If not defined then name of<typeparamref name="T"/> is used</paramref>
        /// <returns></returns>
        bool TryGetDelegate<T>(out T nativeDelegate, string defName = null)
            where T : class, ISerializable, ICloneable; // is delegate


        
    }
}