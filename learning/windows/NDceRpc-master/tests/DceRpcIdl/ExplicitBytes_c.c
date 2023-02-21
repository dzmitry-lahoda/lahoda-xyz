

/* this ALWAYS GENERATED file contains the RPC client stubs */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Tue May 19 20:23:54 2015
 */
/* Compiler settings for ExplicitBytes.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#if defined(_M_AMD64)


#pragma warning( disable: 4049 )  /* more than 64k source lines */
#if _MSC_VER >= 1200
#pragma warning(push)
#endif

#pragma warning( disable: 4211 )  /* redefine extern to static */
#pragma warning( disable: 4232 )  /* dllimport identity*/
#pragma warning( disable: 4024 )  /* array to pointer mapping*/

#include <string.h>

#include "ExplicitBytes_h.h"

#define TYPE_FORMAT_STRING_SIZE   39                                
#define PROC_FORMAT_STRING_SIZE   61                                
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   0            

typedef struct _ExplicitBytes_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } ExplicitBytes_MIDL_TYPE_FORMAT_STRING;

typedef struct _ExplicitBytes_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } ExplicitBytes_MIDL_PROC_FORMAT_STRING;

typedef struct _ExplicitBytes_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } ExplicitBytes_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};


extern const ExplicitBytes_MIDL_TYPE_FORMAT_STRING ExplicitBytes__MIDL_TypeFormatString;
extern const ExplicitBytes_MIDL_PROC_FORMAT_STRING ExplicitBytes__MIDL_ProcFormatString;
extern const ExplicitBytes_MIDL_EXPR_FORMAT_STRING ExplicitBytes__MIDL_ExprFormatString;

#define GENERIC_BINDING_TABLE_SIZE   0            


/* Standard interface: ExplicitBytes, ver. 1.0,
   GUID={0x00000002,0xEAF3,0x4A7A,{0xA0,0xF2,0xBC,0xE4,0xC3,0x0D,0xA7,0x7E}} */



static const RPC_CLIENT_INTERFACE ExplicitBytes___RpcClientInterface =
    {
    sizeof(RPC_CLIENT_INTERFACE),
    {{0x00000002,0xEAF3,0x4A7A,{0xA0,0xF2,0xBC,0xE4,0xC3,0x0D,0xA7,0x7E}},{1,0}},
    {{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}},
    0,
    0,
    0,
    0,
    0,
    0x00000000
    };
RPC_IF_HANDLE ExplicitBytes_v1_0_c_ifspec = (RPC_IF_HANDLE)& ExplicitBytes___RpcClientInterface;

extern const MIDL_STUB_DESC ExplicitBytes_StubDesc;

static RPC_BINDING_HANDLE ExplicitBytes__MIDL_AutoBindHandle;


/* [fault_status][comm_status] */ error_status_t ExplicitBytesExecute( 
    /* [in] */ handle_t clientHandle,
    /* [in] */ long szInput,
    /* [size_is][in] */ const byte input[  ],
    /* [out] */ long *szOutput,
    /* [size_is][size_is][out] */ byte **output)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&ExplicitBytes_StubDesc,
                  (PFORMAT_STRING) &ExplicitBytes__MIDL_ProcFormatString.Format[0],
                  clientHandle,
                  szInput,
                  input,
                  szOutput,
                  output);
    return ( error_status_t  )_RetVal.Simple;
    
}


#if !defined(__RPC_WIN64__)
#error  Invalid build platform for this stub.
#endif

static const ExplicitBytes_MIDL_PROC_FORMAT_STRING ExplicitBytes__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure ExplicitBytesExecute */

			0x0,		/* 0 */
			0x68,		/* Old Flags:  comm or fault/decode */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x0 ),	/* 0 */
/*  8 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 10 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 12 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 14 */	NdrFcShort( 0x8 ),	/* 8 */
/* 16 */	NdrFcShort( 0x24 ),	/* 36 */
/* 18 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x5,		/* 5 */
/* 20 */	0xa,		/* 10 */
			0x7,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 22 */	NdrFcShort( 0x1 ),	/* 1 */
/* 24 */	NdrFcShort( 0x1 ),	/* 1 */
/* 26 */	NdrFcShort( 0x0 ),	/* 0 */
/* 28 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter clientHandle */

/* 30 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 32 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 34 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter szInput */

/* 36 */	NdrFcShort( 0xb ),	/* Flags:  must size, must free, in, */
/* 38 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 40 */	NdrFcShort( 0x2 ),	/* Type Offset=2 */

	/* Parameter input */

/* 42 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 44 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 46 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter szOutput */

/* 48 */	NdrFcShort( 0x2013 ),	/* Flags:  must size, must free, out, srv alloc size=8 */
/* 50 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 52 */	NdrFcShort( 0x12 ),	/* Type Offset=18 */

	/* Parameter output */

/* 54 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 56 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 58 */	0x10,		/* FC_ERROR_STATUS_T */
			0x0,		/* 0 */

			0x0
        }
    };

static const ExplicitBytes_MIDL_TYPE_FORMAT_STRING ExplicitBytes__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */
/*  2 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/*  4 */	NdrFcShort( 0x1 ),	/* 1 */
/*  6 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x0,		/*  */
/*  8 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 10 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 12 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 14 */	
			0x11, 0xc,	/* FC_RP [alloced_on_stack] [simple_pointer] */
/* 16 */	0x8,		/* FC_LONG */
			0x5c,		/* FC_PAD */
/* 18 */	
			0x11, 0x14,	/* FC_RP [alloced_on_stack] [pointer_deref] */
/* 20 */	NdrFcShort( 0x2 ),	/* Offset= 2 (22) */
/* 22 */	
			0x12, 0x0,	/* FC_UP */
/* 24 */	NdrFcShort( 0x2 ),	/* Offset= 2 (26) */
/* 26 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 28 */	NdrFcShort( 0x1 ),	/* 1 */
/* 30 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x54,		/* FC_DEREFERENCE */
/* 32 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 34 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 36 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */

			0x0
        }
    };

static const unsigned short ExplicitBytes_FormatStringOffsetTable[] =
    {
    0
    };


static const COMM_FAULT_OFFSETS ExplicitBytes_CommFaultOffsets[] = 
{
	{ -1, -1 } 	/* ia64 Offsets for ExplicitBytesExecute */
};


static const MIDL_STUB_DESC ExplicitBytes_StubDesc = 
    {
    (void *)& ExplicitBytes___RpcClientInterface,
    MIDL_user_allocate,
    MIDL_user_free,
    &ExplicitBytes__MIDL_AutoBindHandle,
    0,
    0,
    0,
    0,
    ExplicitBytes__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x50002, /* Ndr library version */
    0,
    0x800025b, /* MIDL Version 8.0.603 */
    ExplicitBytes_CommFaultOffsets,
    0,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* defined(_M_AMD64)*/

