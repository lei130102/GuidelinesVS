#pragma once

#include "pch.h"

extern "C"
{
	//����int������int
	DLLEXPORT int __stdcall func_int_int(int param);

	//��Ϊ��������int
	DLLEXPORT void __stdcall func_void_int_ptr(int* param);

	//����char������char
	DLLEXPORT char __stdcall func_char_char(char param);

	//��Ϊ��������char
	DLLEXPORT void __stdcall func_void_char_ptr(char* param);

	//����char*
	DLLEXPORT void __stdcall func_void_char_ptr(char* param);
	DLLEXPORT void __stdcall func_void_char_const_ptr(char const* param);

	//����wchar_t*
	DLLEXPORT void __stdcall func_void_wchar_t_ptr(wchar_t* param);
	DLLEXPORT void __stdcall func_void_wchar_t_const_ptr(wchar_t const* param);

	//����char*

	//����wchar_t*
}
