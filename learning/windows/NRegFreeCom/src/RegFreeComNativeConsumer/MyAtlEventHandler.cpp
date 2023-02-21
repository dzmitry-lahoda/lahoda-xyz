#include "StdAfx.h"
#include "MyAtlEventHandler.h"
#include <assert.h>
#include <iostream>

MyAtlEventHandler::MyAtlEventHandler(void)
    :m_refCount(1)
{
    
}


MyAtlEventHandler::~MyAtlEventHandler(void)
{
}


_ATL_FUNC_INFO MyAtlEventHandler:: m_FloatPropertyChangingInfo = {CC_STDCALL, VT_EMPTY, 2 , {VT_R4, VT_VARIANT}};
_ATL_FUNC_INFO MyAtlEventHandler:: m_PassStringInfo = {CC_STDCALL, VT_EMPTY, 1 , {VT_BSTR}};
_ATL_FUNC_INFO MyAtlEventHandler:: m_SimpleEmptyEventInfo = {CC_STDCALL, VT_EMPTY,0 , {}};

STDMETHODIMP MyAtlEventHandler::FloatPropertyChanging(float NewValue, bool* Cancel)
{
	std::cout << "FloatPropertyChanging" << std::endl;
	*Cancel = true;
    return S_OK;
}

void MyAtlEventHandler::Subscribe(IUnknown* unk){
 HRESULT hr = DispEventAdvise(unk);
 ATLASSERT(hr == S_OK);
}

STDMETHODIMP MyAtlEventHandler::PassStuct(RegFreeCom_Interfaces::MyCoolStuct val)
{
	std::cout << "PassStuct" << std::endl;
	auto valval = val._Val;
	auto valval2 = val._Val2;
	m_val = val;
    return S_OK;
}

STDMETHODIMP MyAtlEventHandler::EnsureGCIsNotObstacle()
{
	PassStuct(m_val);
	PassClass(m_obj);
	PassString(m_str);
    return S_OK;
}


STDMETHODIMP MyAtlEventHandler::SimpleEmptyEvent()
{
	std::cout << "SimpleEmptyEvent" << std::endl;
    return S_OK;
}

STDMETHODIMP MyAtlEventHandler::PassString(BSTR str)
{
	std::cout << "PassString" << std::endl;
	m_str = str;
    return S_OK;
}

STDMETHODIMP MyAtlEventHandler::PassClass(RegFreeCom_Interfaces::IMyCoolClass* obj)
{
	std::cout << "PassClass" << std::endl;
	BSTR val;
	auto mv = obj->GetMyValue2(&val);
	auto hr = obj->get_MyValue(&val);
	auto val2 = obj->MyValue;
	auto val3 = obj->GetMyValue();
	m_obj = obj;
    return S_OK;
}




