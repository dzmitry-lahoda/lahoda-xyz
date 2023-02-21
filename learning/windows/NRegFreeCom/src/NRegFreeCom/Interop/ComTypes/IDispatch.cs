﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace NRegFreeCom.Interop.ComTypes
{

    /// <summary>
    /// A partial declaration of IDispatch used to lookup Type information and DISPIDs.
    /// </summary>
    /// <remarks>
    /// This interface only declares the first three methods of IDispatch.  It omits the
    /// fourth method (Invoke) because there are already plenty of ways to do dynamic
    /// invocation in .NET.  But the first three methods provide dynamic type metadata
    /// discovery, which .NET doesn't provide normally if you have a System.__ComObject
    /// RCW instead of a strongly-typed RCW.
    /// <para/>
    /// Note: The original declaration of IDispatch is in OAIdl.idl.
    /// </remarks>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(WELL_KNOWN_IIDS.IID_IDispatch)]
    public interface IDispatch
    {
        /// <summary>
        /// Gets the number of Types that the object provides (0 or 1).
        /// </summary>
        /// <param name="typeInfoCount">Returns 0 or 1 for the number of Types provided by <see cref="GetTypeInfo"/>.</param>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/da876d53-cb8a-465c-a43e-c0eb272e2a12(VS.85)
        /// </remarks>
        [PreserveSig]
        int GetTypeInfoCount(out int typeInfoCount);

        /// <summary>
        /// Gets the Type information for an object if <see cref="GetTypeInfoCount"/> returned 1.
        /// </summary>
        /// <param name="typeInfoIndex">Must be 0.</param>
        /// <param name="lcid">Typically, LOCALE_SYSTEM_DEFAULT (2048).</param>
        /// <param name="typeInfo">Returns the object's Type information.</param>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/cc1ec9aa-6c40-4e70-819c-a7c6dd6b8c99(VS.85)
        /// </remarks>
        void GetTypeInfo(int typeInfoIndex, int lcid, [MarshalAs(UnmanagedType.CustomMarshaler,
                                                          MarshalTypeRef = typeof(System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler))] out Type typeInfo);

        /// <summary>
        /// Gets the DISPID of the specified member name.
        /// </summary>
        /// <param name="riid">Must be IID_NULL.  Pass a copy of Guid.Empty.</param>
        /// <param name="name">The name of the member to look up.</param>
        /// <param name="nameCount">Must be 1.</param>
        /// <param name="lcid">Typically, LOCALE_SYSTEM_DEFAULT (2048).</param>
        /// <param name="dispId">If a member with the requested <paramref name="name"/>
        /// is found, this returns its DISPID and the method's return value is 0.
        /// If the method returns a non-zero value, then this parameter's output value is
        /// undefined.</param>
        /// <returns>Zero for success. Non-zero for failure.</returns>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/6f6cf233-3481-436e-8d6a-51f93bf91619(VS.85)
        /// </remarks>
        [PreserveSig]
        int GetIDsOfNames(ref Guid riid, ref string name, int nameCount, int lcid, out int dispId);

        // NOTE: The real IDispatch also has an Invoke method next, but we don't need it.
        // We can invoke methods using .NET's Type.InvokeMember method with the special
        // [DISPID=n] syntax for member "names", or we can get a .NET Type using GetTypeInfo
        // and invoke methods on that through reflection.
        // Type.InvokeMember: http://msdn.microsoft.com/en-us/library/de3dhzwy.aspx
    }
}