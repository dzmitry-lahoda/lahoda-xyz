

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 06:14:07 2038
 */
/* Compiler settings for RegFreeComNativeImplementer.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        EXTERN_C __declspec(selectany) const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif // !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, IID_IImplementer,0x156489B3,0x1745,0x48D3,0x9F,0xAA,0x2C,0x58,0xDB,0xA0,0x1D,0x53);


MIDL_DEFINE_GUID(IID, LIBID_RegFreeComNativeImplementerLib,0xDA4AE577,0xB161,0x4D4A,0x9E,0x55,0x02,0x21,0xD9,0x49,0x20,0x3C);


MIDL_DEFINE_GUID(CLSID, CLSID_Implementer,0x538ECD5D,0x8A57,0x4F1C,0xAE,0xB1,0xEB,0xC4,0x25,0x64,0x1F,0x0B);


MIDL_DEFINE_GUID(CLSID, CLSID_MyService,0x878CCCCB,0x8F7C,0x46A6,0xA5,0xC7,0x6B,0xA4,0x24,0x8C,0x1E,0x6E);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



