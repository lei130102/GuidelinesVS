#include "pch.h"
#include "test.h"

#include <cstdio>

extern "C"
{
	DLLEXPORT int __stdcall func_int_int(int param)
	{
		return 2 * param;
	}

	DLLEXPORT void __stdcall func_void_int_ptr(int* param)
	{
		*param = 1;
	}

	DLLEXPORT char __stdcall func_char_char(char param)
	{
		return param + 1;
	}

	DLLEXPORT void __stdcall func_void_char_ptr(char* param)
	{
		*param = 'c';
	}

	DLLEXPORT void __stdcall func_void_char_ptr(char* param)
	{
		FILE* fp = std::fopen("func_void_char_ptr.txt", "w");
		if (fp != nullptr)
		{
			std::fwrite(param, sizeof(char), strlen(param), fp);

			std::fclose(fp);
		}
	}

	DLLEXPORT void __stdcall func_void_char_const_ptr(char const* param)
	{
		FILE* fp = std::fopen("func_void_char_const_ptr.txt", "w");
		if (fp != nullptr)
		{
			std::fwrite(param, sizeof(char), strlen(param), fp);

			std::fclose(fp);
		}
	}

	DLLEXPORT void __stdcall func_void_wchar_t_ptr(wchar_t* param)
	{
		FILE* fp = std::fopen("func_void_wchar_t_ptr.txt", "w");
		if (fp != nullptr)
		{
			std::fwrite(param, sizeof(wchar_t), wcslen(param), fp);

			std::fclose(fp);
		}
	}

	DLLEXPORT void __stdcall func_void_wchar_t_const_ptr(wchar_t const* param)
	{
		FILE* fp = std::fopen("func_void_wchar_t_const_ptr.txt", "w");
		if (fp != nullptr)
		{
			std::fwrite(param, sizeof(wchar_t), wcslen(param), fp);

			std::fclose(fp);
		}
	}
}