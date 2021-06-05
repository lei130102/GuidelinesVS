#pragma once

#include "pch.h"

extern "C"
{
	//传递int并返回int
	DLLEXPORT int __stdcall func_int_int(int param);

	//作为参数返回int
	DLLEXPORT void __stdcall func_void_int_ptr(int* param);

	//传递char并返回char
	DLLEXPORT char __stdcall func_char_char(char param);

	//作为参数返回char
	DLLEXPORT void __stdcall func_void_char_ptr(char* param);

	//传递char*
	DLLEXPORT void __stdcall func_void_char_ptr(char* param);
	DLLEXPORT void __stdcall func_void_char_const_ptr(char const* param);

	//传递wchar_t*
	DLLEXPORT void __stdcall func_void_wchar_t_ptr(wchar_t* param);
	DLLEXPORT void __stdcall func_void_wchar_t_const_ptr(wchar_t const* param);

	//返回char*

	//返回wchar_t*
}
