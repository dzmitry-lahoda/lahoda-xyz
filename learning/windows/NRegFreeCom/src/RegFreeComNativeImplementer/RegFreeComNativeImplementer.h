

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


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



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __RegFreeComNativeImplementer_h__
#define __RegFreeComNativeImplementer_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IImplementer_FWD_DEFINED__
#define __IImplementer_FWD_DEFINED__
typedef interface IImplementer IImplementer;

#endif 	/* __IImplementer_FWD_DEFINED__ */


#ifndef __Implementer_FWD_DEFINED__
#define __Implementer_FWD_DEFINED__

#ifdef __cplusplus
typedef class Implementer Implementer;
#else
typedef struct Implementer Implementer;
#endif /* __cplusplus */

#endif 	/* __Implementer_FWD_DEFINED__ */


#ifndef __MyService_FWD_DEFINED__
#define __MyService_FWD_DEFINED__

#ifdef __cplusplus
typedef class MyService MyService;
#else
typedef struct MyService MyService;
#endif /* __cplusplus */

#endif 	/* __MyService_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "RegFreeComNativeInterfaces.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IImplementer_INTERFACE_DEFINED__
#define __IImplementer_INTERFACE_DEFINED__

/* interface IImplementer */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IImplementer;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("156489B3-1745-48D3-9FAA-2C58DBA01D53")
    IImplementer : public IDispatch
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IImplementerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IImplementer * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IImplementer * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IImplementer * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IImplementer * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IImplementer * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IImplementer * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IImplementer * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } IImplementerVtbl;

    interface IImplementer
    {
        CONST_VTBL struct IImplementerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IImplementer_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IImplementer_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IImplementer_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IImplementer_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IImplementer_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IImplementer_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IImplementer_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IImplementer_INTERFACE_DEFINED__ */



#ifndef __RegFreeComNativeImplementerLib_LIBRARY_DEFINED__
#define __RegFreeComNativeImplementerLib_LIBRARY_DEFINED__

/* library RegFreeComNativeImplementerLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_RegFreeComNativeImplementerLib;

EXTERN_C const CLSID CLSID_Implementer;

#ifdef __cplusplus

class DECLSPEC_UUID("538ECD5D-8A57-4F1C-AEB1-EBC425641F0B")
Implementer;
#endif

EXTERN_C const CLSID CLSID_MyService;

#ifdef __cplusplus

class DECLSPEC_UUID("878CCCCB-8F7C-46A6-A5C7-6BA4248C1E6E")
MyService;
#endif
#endif /* __RegFreeComNativeImplementerLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


