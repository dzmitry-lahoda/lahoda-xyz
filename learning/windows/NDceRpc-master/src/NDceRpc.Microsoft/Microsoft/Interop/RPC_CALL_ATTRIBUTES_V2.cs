﻿using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RPC_CALL_ATTRIBUTES_V2
    {
        public uint Version;
        public RPC_CALL_ATTRIBUTES_FLAGS Flags;
        public int ServerPrincipalNameBufferLength;
        public IntPtr ServerPrincipalName;
        public int ClientPrincipalNameBufferLength;
        public IntPtr ClientPrincipalName;
        public RPC_C_AUTHN_LEVEL AuthenticationLevel;
        public RPC_C_AUTHN AuthenticationService;
        public bool NullSession;
        public bool KernelMode;
        public RpcProtoseqType ProtocolSequence;
        public RpcCallClientLocality IsClientLocal;
        public IntPtr ClientPID;
        public RpcCallStatus CallStatus;
        public RpcCallType CallType;
        public IntPtr CallLocalAddress;
        public short OpNum;
        public Guid InterfaceUuid;
    }
}